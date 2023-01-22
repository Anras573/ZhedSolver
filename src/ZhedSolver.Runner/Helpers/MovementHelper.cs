using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.Helpers;

public static class MovementHelper
{
    public static List<Vector2> MoveAndGetMovement(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited)
    {
        var moves = new List<Vector2>(steps);
        
        while (steps != 0)
        {
            position += direction;
            
            if (visited.Contains(position))
                continue;

            visited.Add(position);
            moves.Add(position);
            steps--;
        }

        return moves;
    }

    public static (bool, List<Vector2>) TryMoveAndGetMovement(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited, Bounds bounds)
    {
        bool OutOfBounds()
        {
            return position.X > bounds.Max.X
               || position.X < bounds.Min.X
               || position.Y > bounds.Max.Y
               || position.Y < bounds.Min.Y;
        }
        
        var moves = new List<Vector2>(steps);
        
        while (steps != 0)
        {
            position += direction;

            if (OutOfBounds())
                return (false, moves);
            
            if (visited.Contains(position))
                continue;

            visited.Add(position);
            moves.Add(position);
            steps--;
        }

        return (true, moves);
    }
    
    public static Vector2 MoveAndGetLastPosition(Vector2 position, Vector2 direction, int steps, HashSet<Vector2> visited)
    {
        while (steps != 0)
        {
            position += direction;
            
            if (visited.Contains(position))
                continue;

            visited.Add(position);
            steps--;
        }

        return position;
    }
}