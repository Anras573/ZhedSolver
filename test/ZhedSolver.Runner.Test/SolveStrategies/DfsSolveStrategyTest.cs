using System.Numerics;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class DfsSolveStrategyTest
{
    [Fact]
    public void SolveLevel1()
    {
        var goal = new Vector2(4, 3);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 1 } };
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal);

        var expected = new List<Step> { new (new Vector2(3, 3), 1, Direction.Right) };
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void SolveLevel2()
    {
        var goal = new Vector2(3, 5);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 2 } };
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal);

        var expected = new List<Step> { new (new Vector2(3, 3), 2, Direction.Down) };
        Assert.Equal(expected, actual);
    }
}