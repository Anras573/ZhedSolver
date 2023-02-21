using ZhedSolver.Runner.Test.TestData;
using ZhedSolver.Runner.Test.TestHelpers;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class ParallelBfsSolveStrategyTest
{
    [Theory]
    [ClassData(typeof(ZhedTestLevelData))]
    public void SolveLevels(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new ParallelBfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);
        
        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);
        
        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory]
    [ClassData(typeof(Level11TestData))]
    public void SolveLevel11(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new DfsSolveStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);
        
        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);
        
        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory]
    [ClassData(typeof(Level14TestData))]
    public void SolveLevel14(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new ParallelPermutationStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory]
    [ClassData(typeof(Level16TestData))]
    public void SolveLevel16(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new ParallelPermutationStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
    
    [Theory]
    [ClassData(typeof(Level29TestData))]
    public void SolveLevel29(Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds)
    {
        var sut = new ParallelPermutationStrategy();

        var actual = sut.Solve(map, goal, bounds);

        Assert.NotEmpty(actual);

        var solutionFound = ZhedSolverTestHelper.SolutionFound(expected, actual);

        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
}