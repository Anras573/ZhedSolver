using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class DfsSolveStrategy : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal)
    {
        var steps = new List<Step>();
        var visited = map.Keys.ToHashSet();

        steps = Dfs(map, goal, steps, visited);

        return steps;
    }

    private static List<Step> Dfs(Dictionary<Vector2, int> map, Vector2 goal, List<Step> steps, HashSet<Vector2> visited)
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
            
            // Direction right
            var moves = Move(position, Directions.Right, value, visited);
            steps.Add(new Step(position, value, Direction.Right));

            if (moves[^1] == goal) 
                return steps;

            var solution = Dfs(nextMap, goal, steps, visited);
            
            if (solution.Count > 0)
                return solution;
            
            Rewind(moves);

            // Direction down
            moves = Move(position, Directions.Down, value, visited);
            steps.Add(new Step(position, value, Direction.Down));

            if (moves[^1] == goal) 
                return steps;

            solution = Dfs(nextMap, goal, steps, visited);
            
            if (solution.Count > 0)
                return solution;
            
            Rewind(moves);

            // Direction up
            moves = Move(position, Directions.Up, value, visited);
            steps.Add(new Step(position, value, Direction.Up));

            if (moves[^1] == goal) 
                return steps;

            solution = Dfs(nextMap, goal, steps, visited);
            
            if (solution.Count > 0)
                return solution;
            
            Rewind(moves);

            // Direction left
            moves = Move(position, Directions.Left, value, visited);
            steps.Add(new Step(position, value, Direction.Left));

            if (moves[^1] == goal) 
                return steps;

            solution = Dfs(nextMap, goal, steps, visited);

            if (solution.Count > 0)
                return solution;
            
            Rewind(moves);
        }

        return Array.Empty<Step>().ToList();
    }

    private static List<Vector2> Move(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited)
    {
        var moves = new List<Vector2>();
        
        while (steps != 0)
        {
            position += direction;
            
            if (visited.Contains(position))
                continue;

            visited.Add(position);
            moves.Add(position);
            steps--;
        }

        return moves;
    }
}