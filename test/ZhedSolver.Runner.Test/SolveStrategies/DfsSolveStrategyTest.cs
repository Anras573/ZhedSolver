using System.Numerics;
using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class DfsSolveStrategyTest
{
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected)
    {
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal);
        
        Assert.Equal(expected, actual);
    }

    [Fact(Skip = "Code not implemented yet.")]
    public void SolveLevel3()
    {
        var goal = new Vector2(5, 4);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(2, 5), 1 },
            { new Vector2(3, 5), 1 }
        };
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal);

        var expected = new List<Step>
        {
            new (new Vector2(3, 5), 1, Direction.Right),
            new (new Vector2(2, 5), 1, Direction.Right)
        };
        Assert.Equal(expected, actual);
    }
}