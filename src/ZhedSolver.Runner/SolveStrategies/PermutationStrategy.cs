using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.Utils;

namespace ZhedSolver.Runner.SolveStrategies;

public class PermutationStrategy : ISolveStrategy
{
    private Dictionary<Vector2, int> _valueLookup = new ();
    private Bounds _bounds;

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _valueLookup = map;
        _bounds = bounds;
        
        var coordinates = map.Select(kv => kv.Key).ToList();
        var permutations = coordinates.GetPermutations(coordinates.Count);

        bool EndsWithPossibleSolution(IEnumerable<Vector2> permutation)
        {
            var candidate = permutation.Last();
            return candidate.X.Equals(goal.X) || candidate.Y.Equals(goal.Y);
        }
        
        foreach (var permutation in permutations.Where(EndsWithPossibleSolution))
        {
            var permutationList = permutation.ToList();
            var visited = new HashSet<Vector2>(permutationList);
            var steps = Dfs(permutationList, goal, new List<Step>(), visited);

            if (steps.Any())
                return steps;
        }
        
        return new List<Step>();
    }
    
    private List<Step> Dfs(List<Vector2> map, Vector2 goal, List<Step> steps, HashSet<Vector2> visited)
    {
        void Rewind(List<Vector2> moves)
        {
            steps.RemoveAt(steps.Count - 1);
            visited.RemoveWhere(moves.Contains);
        }

        if (!map.Any())
            return Array.Empty<Step>().ToList();

        var position = map.First();
        var value = _valueLookup[position];
        
        var nextMap = map
            .Where(p => p != position)
            .ToList();

        if (position.X != _bounds.Max.X)
        {
            // Direction right
            var moves = Move(position, Directions.Right, value, visited);
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
            var moves = Move(position, Directions.Down, value, visited);
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
            var moves = Move(position, Directions.Left, value, visited);
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
            var moves = Move(position, Directions.Up, value, visited);
            steps.Add(new Step(position, value, Direction.Up));

            if (moves[^1] == goal) 
                return steps;

            var solution = Dfs(nextMap, goal, steps, visited);
        
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