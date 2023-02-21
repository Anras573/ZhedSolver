using System.Collections.Concurrent;
using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class ParallelBfsSolveStrategy : ISolveStrategy
{
    private readonly Dictionary<Vector2, Direction> _directionMap = new()
    {
        { Directions.Down, Direction.Down },
        { Directions.Left, Direction.Left },
        { Directions.Right, Direction.Right },
        { Directions.Up, Direction.Up }
    };
    
    private Bounds _bounds;
    
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        var visited = map.Keys.ToHashSet();
        _bounds = bounds;
        
        return Bfs(map, goal, visited);
    }

    private List<Step> Bfs(Dictionary<Vector2, int> map, Vector2 goal, HashSet<Vector2> visited)
    {
        var stepsOfSteps = new ConcurrentBag<List<Step>>();
        var queue = new ConcurrentQueue<State>();

        foreach (var (position, value) in map)
        {
            var nextMap = map
                .Where(kv => kv.Key != position)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (var dir in GetDirections(position, _bounds))
            {
                var newVisited = new HashSet<Vector2>(visited);
                var newPosition = MovementHelper.MoveAndGetLastPosition(position, dir, value, newVisited);
                var newPath = new List<Step> { new (position, value, _directionMap[dir]) };

                if (newPosition.Equals(goal))
                    return newPath;
                
                queue.Enqueue(new State(nextMap, newVisited, newPath, newPosition));
            }
        }
        
        Parallel.For(0, 8, (i, p) =>
        {
            while (queue.TryDequeue(out var state))
            {
                if (state.Position == goal)
                {
                    stepsOfSteps.Add(state.Steps);
                    p.Break();
                    return;
                }

                foreach (var (position, value) in state.Map)
                {
                    var nextMap = state.Map
                        .Where(kv => kv.Key != position)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);

                    foreach (var dir in GetDirections(position, _bounds))
                    {
                        var newVisited = new HashSet<Vector2>(state.Visited);
                        var newPosition = MovementHelper.MoveAndGetLastPosition(position, dir, value, newVisited);
                        var newPath = new List<Step>(state.Steps) { new (position, value, _directionMap[dir]) };

                        if (newPosition == goal)
                        {
                            stepsOfSteps.Add(newPath);
                            p.Break();
                            return;
                        }

                        queue.Enqueue(new State(nextMap, newVisited, newPath, newPosition));
                    }
                }
            }
        });
        
        return stepsOfSteps.Any() ? stepsOfSteps.First() : Array.Empty<Step>().ToList();
    }
    
    private static IEnumerable<Vector2> GetDirections(Vector2 position, Bounds bounds)
    {
        if (position.X != bounds.Max.X)
            yield return Directions.Right;

        if (position.Y != bounds.Max.Y)
            yield return Directions.Down;

        if (position.X != bounds.Min.X)
            yield return Directions.Left;

        if (position.Y != bounds.Min.Y)
            yield return Directions.Up;
    }

    private record State(Dictionary<Vector2, int> Map, HashSet<Vector2> Visited, List<Step> Steps, Vector2 Position);
}