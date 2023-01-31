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
        //var file = File.ReadAllLines("input/11.txt");
        const string file =
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
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    [Benchmark]
    public void Level14()
    {
        // var file = File.ReadAllLines("input/14.txt");
        const string file =
"""
----------
---2------
----1-----
--1-------
---1--x---
----1-----
----1-1---
-------1--
-----1----
----------
""";
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    [Benchmark]
    public void Level16()
    {
        // var file = File.ReadAllLines("input/16.txt");
        const string file =
"""
--------
----1---
------1-
-21-----
-----1--
--x--1--
---3----
--------
""";
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
}
