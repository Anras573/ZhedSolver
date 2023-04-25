using BenchmarkDotNet.Attributes;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public class BenchmarkStrategies
{
    const string Level11 =
        """
----------
----------
----------
----123---
---1------
---2------
---3-----x
----------
----------
----------
""";
    
    [Benchmark]
    public void Level11_BFS()
    {
        Parser.Parse(Level11).Solve(new BfsSolveStrategy());
    }
    
    [Benchmark]
    public void Level11_DFS()
    {
        Parser.Parse(Level11).Solve(new DfsSolveStrategy());
    }
    
    [Benchmark]
    public void Level11_Permutation()
    {
        Parser.Parse(Level11).Solve(new PermutationStrategy());
    }
    
    [Benchmark]
    public void Level11_ParallelBFS()
    {
        Parser.Parse(Level11).Solve(new ParallelBfsSolveStrategy());
    }
    
    [Benchmark]
    public void Level11_ParallelPermutation()
    {
        Parser.Parse(Level11).Solve(new ParallelPermutationStrategy());
    }
}