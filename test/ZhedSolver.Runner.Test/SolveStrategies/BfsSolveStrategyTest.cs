using ZhedSolver.Runner.Test.TestData;
using ZhedSolver.Runner.Test.TestHelpers;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class BfsSolveStrategyTest
{
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);
        
        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);
        
        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory]
    [ClassData(typeof(Level11TestData))]
    public void SolveLevel11(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);
        
        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);
        
        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory(Skip = "Takes too long to run")]
    [ClassData(typeof(Level14TestData))]
    public void SolveLevel14(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory(Skip = "Takes too long to run")]
    [ClassData(typeof(Level16TestData))]
    public void SolveLevel16(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory(Skip = "Takes too long to run")]
    [ClassData(typeof(Level29TestData))]
    public void SolveLevel29(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new BfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
}