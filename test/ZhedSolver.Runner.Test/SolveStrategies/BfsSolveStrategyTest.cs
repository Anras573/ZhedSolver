using System.Numerics;
using Xunit.Abstractions;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class BfsSolveStrategyTest
{
    private readonly ITestOutputHelper _output;

    public BfsSolveStrategyTest(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal);

        Assert.False(expected.Except(actual).Any());
    }
}