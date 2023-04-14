namespace ZhedSolver.Runner.Helpers;

public static class PermutationsHelper
{
    public static void HeapPermute(List<Vector2> a, int size, List<List<Vector2>> target, Func<List<Vector2>, bool> predicate)
    {
        if (size == 1 && predicate(a))
        {
            target.Add(new List<Vector2>(a));
        }
        else if (size != 1)
        {
            for (var i = 0; i < size; i++)
            {
                HeapPermute(a, size - 1, target, predicate);
                if (size % 2 == 1)
                {
                    (a[0], a[size - 1]) = (a[size - 1], a[0]);
                }
                else
                {
                    (a[i], a[size - 1]) = (a[size - 1], a[i]);
                }
            }
        }
    }

    public static List<List<Vector2>> GetPossiblePaths(List<Vector2> initialList, Vector2 goal)
    {
        bool IsWithinRange(Vector2 a, Vector2 b, Vector2 c)
        {
            return (a.X > MathF.Min(b.X, c.X) && a.X < MathF.Max(b.X, c.X))
                   || (a.Y > MathF.Min(b.Y, c.Y) && a.Y < MathF.Max(b.Y, c.Y));
        }
        
        var possiblePaths = new List<List<Vector2>>(20_000);

        var list = initialList
            .Where(v => v.X.Equals(goal.X) || v.Y.Equals(goal.Y))
            .Select(v => new List<Vector2>(initialList.Count) { v })
            .ToList();

        var states = new Stack<State>();

        foreach (var l in list)
        {
            var mm = initialList.Except(l).ToList();
            states.Push(new State(mm, l));
        }
        
        Vector2 start, end;

        while (states.TryPop(out var state))
        {
            if (state.PossiblePath.Count < 2)
                (start, end) = (state.PossiblePath[0], goal);
            else
                (start, end) = (state.PossiblePath[^2], state.PossiblePath[^1]);

            var newList = state.MissingMembers
                .Where(v => IsWithinRange(v, start, end)).ToList();
            
            foreach (var l in newList)
            {
                var possiblePath = new List<Vector2>(state.PossiblePath) { l };

                if (possiblePath.Count == initialList.Count)
                {
                    possiblePath.Reverse();
                    possiblePaths.Add(possiblePath);
                }
                else
                {
                    var newState = new State(state.MissingMembers.Where(m => m != l).ToList(), possiblePath);
                    states.Push(newState);
                }
            }
        }
        
        return possiblePaths;
    }
    
    private record State(List<Vector2> MissingMembers, List<Vector2> PossiblePath);
}