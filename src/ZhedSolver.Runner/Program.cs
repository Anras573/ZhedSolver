using System.Diagnostics;
using System.Text;
using BenchmarkDotNet.Running;
using ZhedSolver.Runner;
using ZhedSolver.Runner.Helpers;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

// var files = new[] { "input/11.txt", "input/14.txt", "input/16.txt", "input/29.txt" };
//
// foreach (var filepath in files)
// {
//     var file = File.ReadAllLines(filepath);
//
//     var timestamp = Stopwatch.GetTimestamp();
//
//     var steps = Parser
//         .Parse(file)
//         .Solve(new ParallelPermutationStrategy());
//
//     var sb = new StringBuilder();
//
//     sb.AppendLine(filepath).AppendLine();
//
//     foreach (var line in file)
//     {
//         sb.AppendLine(line);
//     }
//
//     sb.AppendLine();
//
//     foreach (var step in steps)
//     {
//         sb.AppendLine(step.ToString());
//     }
//
//     sb.AppendLine();
//
//     sb.AppendLine($"Execution took: {Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds} ms");
//
//     Console.WriteLine(sb.ToString());
// }
//
// BenchmarkRunner.Run<Benchmarks>();

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
var parsedData = Parser.Parse(file).Solve(new PrintStrategy());

class PrintStrategy : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        Console.WriteLine($"goal: {goal}");
        Console.WriteLine();
        Console.WriteLine($"bounds: {bounds}");
        Console.WriteLine();
        foreach (var kvp in map)
        {
            Console.WriteLine($"coordinate: {kvp.Key}, value: {kvp.Value}");
        }

        return new List<Step>();
    }
}

//Console.ReadKey();
