namespace ZhedSolver.Runner.Test.TestHelpers;

public static class ZhedSolverTestHelper
{
    public static bool SolutionFound(List<List<Step>> expected, List<Step> actual)
    {
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
                return true;
        }

        return false;
    }
}