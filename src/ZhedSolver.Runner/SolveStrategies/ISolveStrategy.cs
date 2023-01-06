using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public interface ISolveStrategy
{
    List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal);
}