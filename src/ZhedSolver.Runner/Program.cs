using System.Diagnostics;
using System.Text;
using ZhedSolver.Runner;
using ZhedSolver.Runner.SolveStrategies;

const string filepath = "input/11.txt";
var timestamp = Stopwatch.GetTimestamp();

var file = File.ReadAllLines(filepath);

var steps = Parser
    .Parse(file)
    .Solve(new DfsSolveStrategy());

var sb = new StringBuilder();

foreach (var line in file)
{
    sb.AppendLine(line);
}

sb.AppendLine();

foreach (var step in steps)
{
    sb.AppendLine(step.ToString());
}

sb.AppendLine();

sb.AppendLine($"Execution took: {Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds} ms");

Console.WriteLine(sb.ToString());
