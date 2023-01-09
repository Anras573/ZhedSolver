using System.Collections.Concurrent;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class BfsSolveStrategy : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal)
    {
        var visited = map.Keys.ToHashSet();
        
        return Bfs(map, goal, visited);
    }

    private static List<Step> Bfs(Dictionary<Vector2, int> map, Vector2 goal, HashSet<Vector2> visited)
    {
        var stepsOfSteps = new ConcurrentBag<List<Step>>();
        var queue = new ConcurrentQueue<State>();

        foreach (var (position, value) in map)
        {
            var nextMap = map
                .Where(kv => kv.Key != position)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        
            // Left
            var newVisited = new HashSet<Vector2>(visited);
            var newPosition = Move(position, Directions.Left, value, newVisited);
            var steps = new List<Step> { new (position, value, Direction.Left) };

            var state = new State(nextMap, newVisited, steps, newPosition);
            queue.Enqueue(state);
        
            // Right
            newVisited = new HashSet<Vector2>(visited);
            newPosition = Move(position, Directions.Right, value, newVisited);
            steps = new List<Step> { new (position, value, Direction.Right) };

            state = new State(nextMap, newVisited, steps, newPosition);
            queue.Enqueue(state);
        
            // Up
            newVisited = new HashSet<Vector2>(visited);
            newPosition = Move(position, Directions.Up, value, newVisited);
            steps = new List<Step> { new (position, value, Direction.Up) };

            state = new State(nextMap, newVisited, steps, newPosition);
            queue.Enqueue(state);
        
            // Down
            newVisited = new HashSet<Vector2>(visited);
            newPosition = Move(position, Directions.Down, value, newVisited);
            steps = new List<Step> { new (position, value, Direction.Down) };

            state = new State(nextMap, newVisited, steps, newPosition);
            queue.Enqueue(state);
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
        
                    // Left
                    var newVisited = new HashSet<Vector2>(state.Visited);
                    var newPosition = Move(position, Directions.Left, value, newVisited);
                    var newSteps = new List<Step>(state.Steps)
                    {
                        new (position, value, Direction.Left)
                    };
                    
                    var newState = new State(nextMap, newVisited, newSteps, newPosition);
                    queue.Enqueue(newState);
        
                    // Right
                    newVisited = new HashSet<Vector2>(state.Visited);
                    newPosition = Move(position, Directions.Right, value, newVisited);
                    newSteps = new List<Step>(state.Steps)
                    {
                        new (position, value, Direction.Right)
                    };

                    newState = new State(nextMap, newVisited, newSteps, newPosition);
                    queue.Enqueue(newState);
        
                    // Up
                    newVisited = new HashSet<Vector2>(state.Visited);
                    newPosition = Move(position, Directions.Up, value, newVisited);
                    newSteps = new List<Step>(state.Steps)
                    {
                        new (position, value, Direction.Up)
                    };

                    newState = new State(nextMap, newVisited, newSteps, newPosition);
                    queue.Enqueue(newState);
        
                    // Down
                    newVisited = new HashSet<Vector2>(state.Visited);
                    newPosition = Move(position, Directions.Down, value, newVisited);
                    newSteps = new List<Step>(state.Steps)
                    {
                        new (position, value, Direction.Down)
                    };

                    newState = new State(nextMap, newVisited, newSteps, newPosition);
                    queue.Enqueue(newState);
                }
            }
        });
        
        return stepsOfSteps.Any() ? stepsOfSteps.First() : Array.Empty<Step>().ToList();
    }
    
    private static Vector2 Move(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited)
    {
        while (steps != 0)
        {
            position += direction;
            
            if (visited.Contains(position))
                continue;

            visited.Add(position);
            steps--;
        }

        return position;
    }

    private record State(Dictionary<Vector2, int> Map, HashSet<Vector2> Visited, List<Step> Steps, Vector2 Position);
}