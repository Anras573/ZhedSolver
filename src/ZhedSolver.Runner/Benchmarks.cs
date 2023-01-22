using BenchmarkDotNet.Attributes;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public class Benchmarks
{
    [Benchmark]
    public void Level14()
    {
        var file = File.ReadAllLines("input/14.txt");
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
}