using System.Collections;

namespace ZhedSolver.Runner.Test.TestData;

public class ZhedTestLevelData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var (goal, map, expected, bounds) = Level1();
        yield return new object[] { goal, map, expected, bounds };
        
        (goal, map, expected, bounds) = Level2();
        yield return new object[] { goal, map, expected, bounds };
        
        (goal, map, expected, bounds) = Level3();
        yield return new object[] { goal, map, expected, bounds };
        
        (goal, map, expected, bounds) = Level4();
        yield return new object[] { goal, map, expected, bounds };
        
        (goal, map, expected, bounds) = Level5();
        yield return new object[] { goal, map, expected, bounds };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static (Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>>, Bounds bounds) Level1()
    {
        var goal = new Vector2(4, 3);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 1 } };
        var expected = new List<Step> { new (new Vector2(3, 3), 1, Direction.Right) };
        var bounds = new Bounds(new Vector2(2, 2), new Vector2(4, 4));

        var expectedList = new List<List<Step>> { expected };

        return (goal, map, expectedList, bounds);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>>, Bounds bounds) Level2()
    {
        var goal = new Vector2(3, 5);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 2 } };
        var expected = new List<Step> { new (new Vector2(3, 3), 2, Direction.Down) };
        var bounds = new Bounds(new Vector2(3, 3), new Vector2(3, 5));

        var expectedList = new List<List<Step>> { expected };

        return (goal, map, expectedList, bounds);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>>, Bounds bounds) Level3()
    {
        var goal = new Vector2(5, 4);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(2, 4), 1 },
            { new Vector2(3, 4), 1 }
        };

        var bounds = new Bounds(new Vector2(2, 4), new Vector2(5, 4));

        var expectedList = new List<List<Step>>
        {
            new ()
            {
                new (new Vector2(2, 4), 1, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right)
            },
            new ()
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(2, 4), 1, Direction.Right)
            }
        };

        return (goal, map, expectedList, bounds);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>>, Bounds bounds) Level4()
    {
        var goal = new Vector2(4, 2);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(2, 4), 2 },
            { new Vector2(4, 5), 2 }
        };
        var expected = new List<Step>
        {
            new (new Vector2(2, 4), 2, Direction.Right),
            new (new Vector2(4, 5), 2, Direction.Up)
        };
        var bounds = new Bounds(new Vector2(2, 2), new Vector2(4, 5));

        var expectedList = new List<List<Step>> { expected };

        return (goal, map, expectedList, bounds);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<List<Step>> expected, Bounds bounds) Level5()
    {
        var goal = new Vector2(6, 2);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(3, 2), 2 },
            { new Vector2(1, 4), 2 },
            { new Vector2(2, 5), 1 },
            { new Vector2(4, 5), 2 }
        };
        var expected = new List<Step>
        {
            new (new Vector2(2, 5), 1, Direction.Up),
            new (new Vector2(1, 4), 2, Direction.Right),
            new (new Vector2(4, 5), 2, Direction.Up),
            new (new Vector2(3, 2), 2, Direction.Right)
        };
        var bounds = new Bounds(new Vector2(1, 2), new Vector2(6, 5));

        var expectedList = new List<List<Step>> { expected };

        return (goal, map, expectedList, bounds);
    }
}