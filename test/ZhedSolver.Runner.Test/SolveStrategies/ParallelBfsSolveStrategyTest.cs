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
        
        var solutionFound = false;
        
        foreach (var exp in expected)
        {
            if (exp.Count != actual.Count) continue;

            var allMatch = true;
            
            for (var i = 0; i < exp.Count; i++)
            {
                if (exp[i] != actual[i])
                    allMatch = false;
            }
            
            if (allMatch)
                solutionFound = true;
        }
        
        Assert.True(solutionFound, $"Actual : {string.Join(Environment.NewLine, actual)}{Environment.NewLine}Is not a valid solution!");
    }
}