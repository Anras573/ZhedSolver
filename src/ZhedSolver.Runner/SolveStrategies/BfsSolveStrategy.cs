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
        var steps = new List<Step>();
        var queue = new ConcurrentQueue<State>();

        var (position, value) = map.First();
        
        var nextMap = map
            .Where(kv => kv.Key != position)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        
        // Left
        var newPosition = Move(position, Directions.Left, value, visited);
        steps.Add(new Step(position, value, Direction.Left));

        var state = new State(nextMap, visited, steps, newPosition);
        queue.Enqueue(state);
        
        // Right
        newPosition = Move(position, Directions.Right, value, visited);
        steps.Add(new Step(position, value, Direction.Right));

        state = new State(nextMap, visited, steps, newPosition);
        queue.Enqueue(state);
        
        // Up
        newPosition = Move(position, Directions.Up, value, visited);
        steps.Add(new Step(position, value, Direction.Up));

        state = new State(nextMap, visited, steps, newPosition);
        queue.Enqueue(state);
        
        // Down
        newPosition = Move(position, Directions.Down, value, visited);
        steps.Add(new Step(position, value, Direction.Down));

        state = new State(nextMap, visited, steps, newPosition);
        queue.Enqueue(state);

        Parallel.For(0, 4, (_) =>
        {
            while (queue.TryDequeue(out var state))
            {
                
            }
        });
        
        return Array.Empty<Step>().ToList();
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