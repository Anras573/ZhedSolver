using System.Numerics;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class BfsSolveStrategyTest
{
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Dictionary<Vector2, int> map, Vector2 goal, List<Step> expected)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal);
        
        Assert.Equal(expected, actual);
    }
}