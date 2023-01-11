using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.SolveStrategies;

public class AStarSolveStrategy : ISolveStrategy
{
    private Bounds _bounds;

    public List<Step> Solve(Dictionary<Vector2, int> map, Vector2 goal, Bounds bounds)
    {
        _bounds = bounds;

        var start = map.Keys.First();
        var openSet = new HashSet<Vector2> { start };
        var cameFrom = new Dictionary<Vector2, Vector2>();
        var gScore = new Dictionary<Vector2, int> { { start, 0 } };
        var fScore = new Dictionary<Vector2, int> { { start, HeuristicCostEstimate(start, goal) } };

        while (openSet.Count > 0)
        {
            var current = openSet.MinBy(v => fScore[v]);

            if (current == goal)
                return ReconstructPath(cameFrom, current, map);

            openSet.Remove(current);
            foreach (var neighbor in GetNeighbors(current, map))
            {
                if (!gScore.ContainsKey(neighbor))
                    gScore.Add(neighbor, int.MaxValue);

                var tentativeGScore = gScore[current] + Distance(current, neighbor);
                if (tentativeGScore >= gScore[neighbor])
                    continue;

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, goal);

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor);
            }
        }
        return Array.Empty<Step>().ToList();
    }

    private int HeuristicCostEstimate(Vector2 a, Vector2 b)
    {
        // Manhattan distance
        return (int)(MathF.Abs(a.X - b.X) + MathF.Abs(a.Y - b.Y));
    }

    private int Distance(Vector2 a, Vector2 b)
    {
        return 1; // assuming the cost of moving from one tile to an adjacent tile is always 1
    }

    private List<Vector2> GetNeighbors(Vector2 current, Dictionary<Vector2, int> map)
    {
        var neighbors = new List<Vector2>();

        if (current.X > _bounds.Min.X)
            neighbors.Add(new Vector2(current.X - 1, current.Y));

        if (current.Y > _bounds.Min.Y)
            neighbors.Add(new Vector2(current.X, current.Y - 1));

        if (current.X < _bounds.Max.X)
            neighbors.Add(new Vector2(current.X + 1, current.Y));

        if (current.Y < _bounds.Max.Y)
            neighbors.Add(new Vector2(current.X, current.Y + 1));

        return neighbors.Where(map.ContainsKey).ToList();
    }
    
    private List<Step> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current, Dictionary<Vector2, int> map)
    {
        var path = new List<Step>();
        var currentStep = current;
        while (cameFrom.ContainsKey(currentStep))
        {
            var previous = cameFrom[currentStep];
            var direction = GetDirection(previous, currentStep);
            path.Add(new Step(previous, map[previous], direction));
            currentStep = previous;
        }
        path.Reverse();
        return path;
    }

    private Direction GetDirection(Vector2 a, Vector2 b)
    {
        if (a.X == b.X)
        {
            if (a.Y < b.Y)
                return Direction.Down;
            return Direction.Up;
        }
        if (a.X < b.X)
            return Direction.Right;
        return Direction.Left;
    }
}