namespace ZhedSolver.Runner.Helpers;

public static class HeuristicsHelper
{
    public static bool EndsWithPossibleSolution(List<Vector2> permutation, Vector2 goal)
    {
        bool IsWithinRange(Vector2 a, Vector2 b, Vector2 c)
        {
            return (a.X > MathF.Min(b.X, c.X) && a.X < MathF.Max(b.X, c.X))
                   || (a.Y > MathF.Min(b.Y, c.Y) && a.Y < MathF.Max(b.Y, c.Y));
        }

        var count = permutation.Count;

        var target = permutation[^1];
            
        if (!(target.X.Equals(goal.X) || target.Y.Equals(goal.Y)))
            return false;
            
        if (count == 1)
            return true;
            
        var a = goal;
        var b = goal;

        var index = 1;
            
        for (var i = permutation.Count - 2; i >= 0; i--)
        {
            if (index != 4)
                index++;
                
            (target, a, b) = (permutation[i], target, a);
                
            if (!IsWithinRange(target, a, b))
                return false;

            if (index >= 4 && count < 7) return true;
        }

        return IsWithinRange(target, a, b);
    }
}