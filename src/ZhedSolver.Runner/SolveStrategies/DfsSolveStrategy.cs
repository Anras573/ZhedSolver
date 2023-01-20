using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class DfsSolveStrategy : ISolveStrategy
{
    private Bounds _bounds;
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _bounds = bounds;
        var steps = new List<Step>(map.Keys.Count);
        var visited = map.Keys.ToHashSet();

        steps = Dfs(map, goal, steps, visited);

        return steps;
    }

    private List<Step> Dfs(Dictionary<Vector2, int> map, Vector2 goal, List<Step> steps, HashSet<Vector2> visited)
    {
        void Rewind(List<Vector2> moves)
        {
            steps.RemoveAt(steps.Count - 1);
            visited.RemoveWhere(moves.Contains);
        }
    
        foreach (var (position, value) in map)
        {
            var nextMap = map
                .Where(kv => kv.Key != position)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            if (position.X != _bounds.Max.X)
            {
                // Direction right
                var moves = MovementHelper.MoveAndGetMovement(position, Directions.Right, value, visited);
                steps.Add(new Step(position, value, Direction.Right));
    
                if (moves[^1] == goal) 
                    return steps;
    
                var solution = Dfs(nextMap, goal, steps, visited);
            
                if (solution.Count > 0)
                    return solution;
            
                Rewind(moves);
            }
            
            if (position.Y != _bounds.Max.Y)
            {
                // Direction down
                var moves = MovementHelper.MoveAndGetMovement(position, Directions.Down, value, visited);
                steps.Add(new Step(position, value, Direction.Down));
    
                if (moves[^1] == goal) 
                    return steps;
    
                var solution = Dfs(nextMap, goal, steps, visited);
            
                if (solution.Count > 0)
                    return solution;
            
                Rewind(moves);
            }
            
            if (position.X != _bounds.Min.X)
            {
                // Direction left
                var moves = MovementHelper.MoveAndGetMovement(position, Directions.Left, value, visited);
                steps.Add(new Step(position, value, Direction.Left));
    
                if (moves[^1] == goal) 
                    return steps;
    
                var solution = Dfs(nextMap, goal, steps, visited);
            
                if (solution.Count > 0)
                    return solution;
            
                Rewind(moves);
            }
            
            if (position.Y != _bounds.Min.Y)
            {
                // Direction up
                var moves = MovementHelper.MoveAndGetMovement(position, Directions.Up, value, visited);
                steps.Add(new Step(position, value, Direction.Up));
    
                if (moves[^1] == goal) 
                    return steps;
    
                var solution = Dfs(nextMap, goal, steps, visited);
            
                if (solution.Count > 0)
                    return solution;
            
                Rewind(moves);
            }
        }
    
        return Array.Empty<Step>().ToList();
    }
}