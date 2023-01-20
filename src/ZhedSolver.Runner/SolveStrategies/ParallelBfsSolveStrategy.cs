using System.Collections.Concurrent;
using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class ParallelBfsSolveStrategy : ISolveStrategy
{
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
        
            if (position.X != _bounds.Max.X)
            {
                // Direction right
                var newVisited = new HashSet<Vector2>(visited);
                var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Right, value, newVisited);
                var steps = new List<Step> { new (position, value, Direction.Right) };

                var state = new State(nextMap, newVisited, steps, newPosition);
                queue.Enqueue(state);
            }
            
            if (position.Y != _bounds.Max.Y)
            {
                // Direction down
                var newVisited = new HashSet<Vector2>(visited);
                var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Down, value, newVisited);
                var steps = new List<Step> { new (position, value, Direction.Down) };

                var state = new State(nextMap, newVisited, steps, newPosition);
                queue.Enqueue(state);
            }
            
            if (position.X != _bounds.Min.X)
            {
                // Direction left
                var newVisited = new HashSet<Vector2>(visited);
                var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Left, value, newVisited);
                var steps = new List<Step> { new (position, value, Direction.Left) };

                var state = new State(nextMap, newVisited, steps, newPosition);
                queue.Enqueue(state);
            }
            
            if (position.Y != _bounds.Min.Y)
            {
                // Direction up
                var newVisited = new HashSet<Vector2>(visited);
                var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Up, value, newVisited);
                var steps = new List<Step> { new (position, value, Direction.Up) };

                var state = new State(nextMap, newVisited, steps, newPosition);
                queue.Enqueue(state);
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
        
                    if (position.X != _bounds.Max.X)
                    {
                        // Direction right
                        var newVisited = new HashSet<Vector2>(state.Visited);
                        var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Right, value, newVisited);
                        var newSteps = new List<Step>(state.Steps)
                        {
                            new (position, value, Direction.Right)
                        };

                        var newState = new State(nextMap, newVisited, newSteps, newPosition);
                        queue.Enqueue(newState);
                    }
            
                    if (position.Y != _bounds.Max.Y)
                    {
                        // Direction down
                        var newVisited = new HashSet<Vector2>(state.Visited);
                        var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Down, value, newVisited);
                        var newSteps = new List<Step>(state.Steps)
                        {
                            new (position, value, Direction.Down)
                        };

                        var newState = new State(nextMap, newVisited, newSteps, newPosition);
                        queue.Enqueue(newState);
                    }
            
                    if (position.X != _bounds.Min.X)
                    {
                        // Direction left
                        var newVisited = new HashSet<Vector2>(state.Visited);
                        var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Left, value, newVisited);
                        var newSteps = new List<Step>(state.Steps)
                        {
                            new (position, value, Direction.Left)
                        };
                    
                        var newState = new State(nextMap, newVisited, newSteps, newPosition);
                        queue.Enqueue(newState);
                    }
            
                    if (position.Y != _bounds.Min.Y)
                    {
                        // Direction up
                        var newVisited = new HashSet<Vector2>(state.Visited);
                        var newPosition = MovementHelper.MoveAndGetLastPosition(position, Directions.Up, value, newVisited);
                        var newSteps = new List<Step>(state.Steps)
                        {
                            new (position, value, Direction.Up)
                        };

                        var newState = new State(nextMap, newVisited, newSteps, newPosition);
                        queue.Enqueue(newState);
                    }
                }
            }
        });
        
        return stepsOfSteps.Any() ? stepsOfSteps.First() : Array.Empty<Step>().ToList();
    }

    private record State(Dictionary<Vector2, int> Map, HashSet<Vector2> Visited, List<Step> Steps, Vector2 Position);
}