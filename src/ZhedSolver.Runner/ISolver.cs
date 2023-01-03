using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public interface ISolver
{
    List<Step> Solve(ISolveStrategy strategy);
}