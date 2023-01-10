using System.Numerics;
using Xunit.Abstractions;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class DfsSolveStrategyTest
{
    private readonly ITestOutputHelper _output;

    public DfsSolveStrategyTest(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected, Bounds bounds)
    {
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.Equal(expected, actual);
    }
}