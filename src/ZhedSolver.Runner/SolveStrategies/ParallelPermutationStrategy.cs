using System.Collections.Concurrent;
using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.Utils;

namespace ZhedSolver.Runner.SolveStrategies;

public class ParallelPermutationStrategy : ISolveStrategy
{
    private Dictionary<Vector2, int> _valueLookup = new ();
    private Bounds _bounds;
    private Vector2 _goal;

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _valueLookup = map;
        _bounds = bounds;
        _goal = goal;
        
        var coordinates = map.Keys.ToList();
        //var permutations = coordinates.Permutations();//coordinates.GetPermutations(coordinates.Count);
        
        var permutations = new List<List<Vector2>>(80640);
        HeapPermute(coordinates, coordinates.Count, permutations);
        
        bool EndsWithPossibleSolution(List<Vector2> permutation)
        {
            bool IsWithinRange(Vector2 a, Vector2 b, Vector2 c)
            {
                return (a.X > MathF.Min(b.X, c.X) && a.X < MathF.Max(b.X, c.X))
                       || (a.Y > MathF.Min(b.Y, c.Y) && a.Y < MathF.Max(b.Y, c.Y));
            }

            var count = permutation.Count;
            var candidate = permutation[^1];
            
            if (!(candidate.X.Equals(goal.X) || candidate.Y.Equals(goal.Y)))
                return false;

            if (count == 1)
                return true;
            
            var candidate2 = permutation[^2];
            if (!IsWithinRange(candidate2, candidate, goal))
                return false;
            
            if (count == 2)
                return true;
            
            var candidate3 = permutation[^3];
            if (!IsWithinRange(candidate3, candidate2, candidate))
                return false;
            
            if (count == 3)
                return true;
            
            var candidate4 = permutation[^4];
            if (!IsWithinRange(candidate4, candidate3, candidate2))
                return false;
            
            if (count < 7)
                return true;
            
            var candidate5 = permutation[^5];
            if (!IsWithinRange(candidate5, candidate4, candidate3))
                return false;
            
            var candidate6 = permutation[^6];
            if (!IsWithinRange(candidate6, candidate5, candidate4))
                return false;
            
            var candidate7 = permutation[^7];
            if (!IsWithinRange(candidate7, candidate6, candidate5))
                return false;
            
            if (count == 7)
                return true;
            
            var candidate8 = permutation[^8];
            if (!IsWithinRange(candidate8, candidate7, candidate6))
                return false;
            
            if (count == 8)
                return true;
            
            var candidate9 = permutation[^9];
            return IsWithinRange(candidate9, candidate8, candidate7);
        }
        
        void HeapPermute(List<Vector2> a, int size, List<List<Vector2>> target)
        {
            if (size == 1 && EndsWithPossibleSolution(a))
            {
                target.Add(new List<Vector2>(a));
            }
            else if (size != 1)
            {
                for (var i = 0; i < size; i++)
                {
                    HeapPermute(a, size - 1, target);
                    if (size % 2 == 1)
                    {
                        (a[0], a[size - 1]) = (a[size - 1], a[0]);
                    }
                    else
                    {
                        (a[i], a[size - 1]) = (a[size - 1], a[i]);
                    }
                }
            }
        }

        var stepsOfSteps = new ConcurrentBag<List<Step>>();
        
        Parallel.ForEach(permutations, (permutation, state) =>
        {
            var visited = permutation.ToHashSet();
            var mapQueue = new Queue<Vector2>(permutation);
            var steps = Dfs(mapQueue, new Stack<Step>(), visited);

            if (steps.Count > 0)
            {
                stepsOfSteps.Add(steps.Reverse().ToList());
                state.Stop();
            }
        });
        
        return stepsOfSteps.FirstOrDefault() ?? Array.Empty<Step>().ToList();
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
            return new Stack<Step>();

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

        return new Stack<Step>();
    }
}