using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class BfsSolveStrategy : ISolveStrategy
{
    private readonly List<Vector2> _directions = new()
    {
        Directions.Left, Directions.Right, Directions.Up, Directions.Down
    };

    private readonly Dictionary<Vector2, Direction> _directionMap = new()
    {
        { Directions.Down, Direction.Down },
        { Directions.Left, Direction.Left },
        { Directions.Right, Direction.Right },
        { Directions.Up, Direction.Up }
    };

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        return Bfs(map, goal, bounds);
    }

    private List<Step> Bfs(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        var start = map.OrderByDescending(kv => kv.Value).ToList();
        var queue = new Queue<State>();
        var visited = new HashSet<Vector2>(map.Select(kv => kv.Key));
        queue.Enqueue(new State(start, visited, new List<Step>()));
        while (queue.Count != 0)
        {
            var state = queue.Dequeue();

            foreach (var field in state.Map)
            {
                var nextMap = state.Map
                    .Where(kv => kv.Key != field.Key)
                    .ToList();
                
                foreach (var dir in GetDirections(field.Key, bounds))
                {
                    var newVisited = new HashSet<Vector2>(state.Visited);
                    var newCoordinate = MovePosition(field.Key, dir, field.Value, newVisited);
                    var newPath = new List<Step>(state.Steps) { new (field.Key, field.Value, _directionMap[dir]) };

                    if (newCoordinate.Equals(goal))
                        return newPath;
                
                    queue.Enqueue(new State(nextMap, newVisited, newPath));
                }
            }
        }
        
        return new List<Step>();
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
    
    private static Vector2 MovePosition(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited)
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
    
    private record State(List<KeyValuePair<Vector2, int>> Map, HashSet<Vector2> Visited, List<Step> Steps);
}