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
    
    public static void HeapPermute<T>(List<T> a, int size, List<List<T>> target, Func<List<T>, bool> predicate)
    {
        if (size == 1 && predicate(a))
        {
            target.Add(new List<T>(a));
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
    
    public static void HeapPermute(List<Vector2> a, int size, List<List<Vector2>> target)
    {
        HeapPermute(a, size, target, _ => true);
    }
}