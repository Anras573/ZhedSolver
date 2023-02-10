using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class PermutationStrategy : ISolveStrategy
{
    private Dictionary<Vector2, int> _valueLookup = new ();
    private static Stack<Step> EmptyStack = new();
    private Bounds _bounds;
    private Vector2 _goal;

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _valueLookup = map;
        _bounds = bounds;
        _goal = goal;
        
        var coordinates = map.Keys.ToList();
        var permutations = new List<List<Vector2>>(2000);
        PermutationsHelper.HeapPermute(
            coordinates,
            coordinates.Count,
            permutations,
            perm => HeuristicsHelper.EndsWithPossibleSolution(perm, goal));
        
        foreach (var permutation in permutations)
        {
            var permutationList = permutation.ToList();
            var visited = new HashSet<Vector2>(permutationList);
            var steps = Dfs(new Queue<Vector2>(permutationList), new Stack<Step>(permutation.Count), visited);

            if (steps.Count > 0)
                return steps.Reverse().ToList();
        }
        
        return new List<Step>();
    }
    
    private Stack<Step> Dfs(Queue<Vector2> map, Stack<Step> steps, HashSet<Vector2> visited)
    {
        void Rewind(List<Vector2> moves, bool shouldPopStack)
        {
            if (shouldPopStack)
                steps.Pop();

            foreach (var move in moves)
            {
                visited.Remove(move);
            }
        }

        if (!map.Any())
            return EmptyStack;

        var position = map.Dequeue();
        var value = _valueLookup[position];

        var lastRound = map.Count == 0;
        var targetDirection = Direction.Right;
        
        if (map.Count == 0)
        {
            if (position.Y.Equals(_goal.Y))
                targetDirection = position.X < _goal.X
                    ? Direction.Right
                    : Direction.Left;
            else if (position.X.Equals(_goal.X))
                targetDirection = position.Y < _goal.Y
                    ? Direction.Down
                    : Direction.Up;
        }

        if (!position.X.Equals(_bounds.Max.X) && (!lastRound || (lastRound && targetDirection == Direction.Right)))
        {
            // Direction right
            var (couldMove, moves) = MovementHelper.TryMoveAndGetMovement(position, Directions.Right, value, visited, _bounds);

            if (couldMove)
            {
                steps.Push(new Step(position, value, Direction.Right));

                if (moves[^1] == _goal) 
                    return steps;

                var solution = Dfs(new Queue<Vector2>(map), steps, visited);
            
                if (solution.Count > 0)
                    return solution;
            }

            Rewind(moves, couldMove);
        }
        
        if (!position.Y.Equals(_bounds.Max.Y) && (!lastRound || (lastRound && targetDirection == Direction.Down)))
        {
            // Direction down
            var (couldMove, moves) = MovementHelper.TryMoveAndGetMovement(position, Directions.Down, value, visited, _bounds);

            if (couldMove)
            {
                steps.Push(new Step(position, value, Direction.Down));

                if (moves[^1] == _goal) 
                    return steps;

                var solution = Dfs(new Queue<Vector2>(map), steps, visited);
        
                if (solution.Count > 0)
                    return solution;
            }
        
            Rewind(moves, couldMove);
        }
        
        if (!position.X.Equals(_bounds.Min.X) && (!lastRound || (lastRound && targetDirection == Direction.Left)))
        {
            // Direction left
            var (couldMove, moves) = MovementHelper.TryMoveAndGetMovement(position, Directions.Left, value, visited, _bounds);

            if (couldMove)
            {
                steps.Push(new Step(position, value, Direction.Left));

                if (moves[^1] == _goal) 
                    return steps;

                var solution = Dfs(new Queue<Vector2>(map), steps, visited);
        
                if (solution.Count > 0)
                    return solution;
            }
        
            Rewind(moves, couldMove);
        }
        
        if (!position.Y.Equals(_bounds.Min.Y) && (!lastRound || (lastRound && targetDirection == Direction.Up)))
        {
            // Direction up
            var (couldMove, moves) = MovementHelper.TryMoveAndGetMovement(position, Directions.Up, value, visited, _bounds);

            if (couldMove)
            {
                steps.Push(new Step(position, value, Direction.Up));

                if (moves[^1] == _goal) 
                    return steps;

                var solution = Dfs(new Queue<Vector2>(map), steps, visited);
        
                if (solution.Count > 0)
                    return solution;
            }
        
            Rewind(moves, couldMove);
        }

        return EmptyStack;
    }
}