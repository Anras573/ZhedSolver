using BenchmarkDotNet.Attributes;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public class Benchmarks
{
    [Benchmark]
    public void Level11()
    {
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
    
    [Benchmark]
    public void Level29()
    {
        const string file =
"""
--------
--2---1-
1---x-1-
--1----1
-2------
-1--1---
------3-
---1-1--
""";
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
    
    // [Benchmark]
    public void Level42()
    {
        const string file =
            """
------------
------------
------1-----
----11-111--
---1-----1--
---1-----1--
--1---x--1--
---1--------
---1--------
---1111-----
------------
------------
""";
        Parser.Parse(file).Solve(new ParallelPermutationStrategy());
    }
}
