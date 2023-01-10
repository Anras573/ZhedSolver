using ZhedSolver.Runner.Models;
using ZhedSolver.Runner.SolveStrategies;

namespace ZhedSolver.Runner;

public class Solver : ISolver
{
    private readonly Dictionary<Vector2, int> _map;
    private readonly Vector2 _goal;
    private readonly Bounds _bounds;

    public Solver(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _map = map;
        _goal = goal;
        _bounds = bounds;
    }
    
    public List<Step> Solve(ISolveStrategy strategy)
    {
        return strategy.Solve(_map, _goal, _bounds);
    }
}