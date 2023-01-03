using System.Numerics;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class DfsSolveStrategy : ISolveStrategy
{
    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal)
    {
        var steps = new List<Step>();
        
        foreach (var (position, value) in map)
        {
            // Direction right
            if (position.Y == goal.Y && position.X + value == goal.X)
            {
                steps.Add(new Step(position, value, Direction.Right));
                return steps;
            }
            
            // Direction down
            if (position.Y + value == goal.Y && position.X == goal.X)
            {
                steps.Add(new Step(position, value, Direction.Down));
                return steps;
            }
        }

        return steps;
    }
}