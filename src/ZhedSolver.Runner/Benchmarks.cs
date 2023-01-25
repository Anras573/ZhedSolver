using BenchmarkDotNet.Attributes;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;
using ZhedSolver.Runner.Utils;

namespace ZhedSolver.Runner;

public class Benchmarks
{
    [Benchmark]
    public void Level11()
    {
        var file = File.ReadAllLines("input/11.txt");
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    [Benchmark]
    public void Level14()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    [Benchmark]
    public void Level16()
    {
        var file = File.ReadAllLines("input/16.txt");
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    // [Benchmark]
    public void Permutations()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new Test());
    }
    
    // [Benchmark]
    public void HeapPermutations()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new Test2());
    }
    
    // [Benchmark]
    public void KnuthShufflePermutations()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new Test3());
    }
}

public class Test : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        bool EndsWithPossibleSolution(IEnumerable<Vector2> permutation)
        {
            var candidate = permutation.Last();
            return candidate.X.Equals(goal.X) || candidate.Y.Equals(goal.Y);
        }
        
        var coordinates = map.Keys.ToList();
        var permutations = coordinates.GetPermutations(coordinates.Count);

        var a = permutations.Where(EndsWithPossibleSolution).Select(p => p.ToList()).ToList();

        return new List<Step>();
    }
}

public class Test2 : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        void HeapPermute(List<Vector2> a, int size, List<List<Vector2>> target)
        {
            switch (size)
            {
                case 1 when EndsWithPossibleSolution(a):
                    target.Add(new List<Vector2>(a));
                    break;
                case 1:
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

                    break;
                }
            }
        }
        
        bool EndsWithPossibleSolution(List<Vector2> permutation)
        {
            var candidate = permutation[^1];
            return candidate.X.Equals(goal.X) || candidate.Y.Equals(goal.Y);
        }
        
        var coordinates = map.Keys.ToList();
        var permutations = new List<List<Vector2>>();
        HeapPermute(coordinates, coordinates.Count, permutations);

        var a = permutations;

        return new List<Step>();
    }
}

public class Test3 : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        bool EndsWithPossibleSolution(IEnumerable<Vector2> permutation)
        {
            var candidate = permutation.Last();
            return candidate.X.Equals(goal.X) || candidate.Y.Equals(goal.Y);
        }
        
        var coordinates = map.Keys.ToList();
        var permutations = coordinates.Permutations().Where(EndsWithPossibleSolution).AsParallel();

        var a = permutations.Select(p => p.ToList()).ToList();

        return new List<Step>();
    }
}
