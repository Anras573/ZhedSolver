using System.Diagnostics;
using ZhedSolver.Runner;

const string filepath = "../../../../../input/11.txt";
var timestamp = Stopwatch.GetTimestamp();

Parser.Parse(File.ReadAllLines(filepath));

Console.WriteLine($"Execution took: {Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds} ms");
