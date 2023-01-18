namespace ZhedSolver.Runner.Test.SolveStrategies;

public class BfsSolveStrategyTest
{
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);
        Assert.False(expected.Except(actual).Any());
    }
}