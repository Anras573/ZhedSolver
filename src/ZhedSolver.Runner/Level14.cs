using BenchmarkDotNet.Attributes;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public class Level14
{
    [Benchmark]
    public void Run()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
}