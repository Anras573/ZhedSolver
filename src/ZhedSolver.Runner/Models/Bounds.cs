namespace ZhedSolver.Runner.Models;

public readonly record struct Bounds(Vector2 Min, Vector2 Max)
{
    public bool OutOfBounds(Vector2 position)
    {
        return position.X > Max.X
               || position.X < Min.X
               || position.Y > Max.Y
               || position.Y < Min.Y;
    }
}