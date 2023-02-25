using System.Collections.Concurrent;
using System.Diagnostics;
using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class ParallelPathPrediction : ISolveStrategy
{
    private Dictionary<Vector2, int> _valueLookup = new ();
    private static Stack<Step> EmptyStack = new();
    private Bounds _bounds;
    private Vector2 _goal;
    
    private Dictionary<Direction, Vector2> _directionMap = new ()
    {
        { Direction.Right, Directions.Right },
        { Direction.Down, Directions.Down },
        { Direction.Left, Directions.Left },
        { Direction.Up, Directions.Up }
    };

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _valueLookup = map;
        _bounds = bounds;
        _goal = goal;
        
        var coordinates = map.Keys.ToList();

        var time = Stopwatch.GetTimestamp();
        
        var permutations = new List<List<Vector2>>(20_000);
        if (coordinates.Count < 7)
        {
            PermutationsHelper.HeapPermute(
                coordinates,
                coordinates.Count,
                permutations,
                perm => HeuristicsHelper.EndsWithPossibleSolution(perm, goal));
        }
        else
        {
            permutations = PermutationsHelper.GetPossiblePaths(coordinates, goal);
        }
        
        Console.WriteLine($"Elapsed time: {Stopwatch.GetElapsedTime(time).TotalMilliseconds}ms");
        
        var stepsOfSteps = new ConcurrentBag<List<Step>>();

        var originalVisited = coordinates.ToHashSet();

        Parallel.ForEach(permutations, (permutation, state) =>
        {
            var visited = new HashSet<Vector2>(originalVisited);
            var mapQueue = new Queue<Vector2>(permutation);
            var steps = PathPrediction(mapQueue, new Stack<Step>(permutation.Count), visited);
            
            if (steps.Count > 0)
            {
                stepsOfSteps.Add(steps.Reverse().ToList());
                state.Stop();
            }
        });

        return stepsOfSteps.FirstOrDefault() ?? Array.Empty<Step>().ToList();
    }

    private Stack<Step> PathPrediction(Queue<Vector2> map, Stack<Step> steps, HashSet<Vector2> visited)
    {
        Direction DirectionPrediction(Vector2 current, Vector2 a, Vector2 b)
        {
            if (current.Y.Equals(a.Y) || current.Y.Equals(b.Y))
            {
                if (current.X < MathF.Max(a.X, b.X))
                    return Direction.Right;
                else if (current.X > MathF.Min(a.X, b.X))
                    return Direction.Left;
                else
                    Debug.Assert(false, $"Current: {current}, a: {a}, b: {b}");
            }
            else if (current.X.Equals(a.X) || current.X.Equals(b.X))
            {
                if (current.Y < MathF.Max(a.Y, b.Y))
                    return Direction.Down;
                else if (current.Y > MathF.Min(a.Y, b.Y))
                    return Direction.Up; 
                else
                    Debug.Assert(false, $"Current: {current}, a: {a}, b: {b}");
            }
            else
                Debug.Assert(false, $"Current: {current}, a: {a}, b: {b}");

            return Direction.Down;
        }

        var (start, end) = (map.Dequeue(), _goal);
        var value = _valueLookup[start];
        var targetDirection = Direction.Right;
        
        if (start.Y.Equals(_goal.Y))
            targetDirection = start.X < _goal.X
                ? Direction.Right
                : Direction.Left;
        else if (start.X.Equals(_goal.X))
            targetDirection = start.Y < _goal.Y
                ? Direction.Down
                : Direction.Up;
        
        MovementHelper.MoveAndGetLastPosition(start, _directionMap[targetDirection], value, visited);
        
        steps.Push(new Step(start, value, targetDirection));

        while (map.TryDequeue(out var position))
        {
            value = _valueLookup[position];
            targetDirection = DirectionPrediction(position, start, end);
            
            MovementHelper.MoveAndGetLastPosition(position, _directionMap[targetDirection], value, visited);
        
            steps.Push(new Step(position, value, targetDirection));

            (start, end) = (end, position);
        }
        
        return steps;
    }
}