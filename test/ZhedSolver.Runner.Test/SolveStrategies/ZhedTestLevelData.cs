using System.Collections;
using System.Numerics;
using ZhedSolver.Runner.Models;

namespace ZhedSolver.Runner.Test.SolveStrategies;

public class ZhedTestLevelData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var (goal, map, expected) = Level1();
        yield return new object[] { goal, map, expected };
        
        (goal, map, expected) = Level2();
        yield return new object[] { goal, map, expected };
        
        (goal, map, expected) = Level3();
        yield return new object[] { goal, map, expected };
        
        (goal, map, expected) = Level4();
        yield return new object[] { goal, map, expected };
        
        (goal, map, expected) = Level5();
        yield return new object[] { goal, map, expected };
        
        (goal, map, expected) = Level11();
        yield return new object[] { goal, map, expected };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level1()
    {
        var goal = new Vector2(4, 3);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 1 } };
        var expected = new List<Step> { new (new Vector2(3, 3), 1, Direction.Right) };

        return (goal, map, expected);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level2()
    {
        var goal = new Vector2(3, 5);
        var map = new Dictionary<Vector2, int> { { new Vector2(3, 3), 2 } };
        var expected = new List<Step> { new (new Vector2(3, 3), 2, Direction.Down) };

        return (goal, map, expected);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level3()
    {
        var goal = new Vector2(5, 4);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(2, 4), 1 },
            { new Vector2(3, 4), 1 }
        };
        var expected = new List<Step>
        {
            new (new Vector2(2, 4), 1, Direction.Right),
            new (new Vector2(3, 4), 1, Direction.Right)
        };

        return (goal, map, expected);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level4()
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

        return (goal, map, expected);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level5()
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

        return (goal, map, expected);
    }
    
    private static (Vector2 goal, Dictionary<Vector2, int> map, List<Step> expected) Level11()
    {
        var goal = new Vector2(9, 6);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(4, 3), 1 },
            { new Vector2(5, 3), 2 },
            { new Vector2(6, 3), 3 },
            { new Vector2(3, 4), 1 },
            { new Vector2(3, 5), 2 },
            { new Vector2(3, 6), 3 }
        };
        var expected = new List<Step>
        {
            new (new Vector2(6, 3), 3, Direction.Down),
            new (new Vector2(3, 4), 1, Direction.Right),
            new (new Vector2(3, 5), 2, Direction.Right),
            new (new Vector2(4, 3), 1, Direction.Down),
            new (new Vector2(5, 3), 2, Direction.Down),
            new (new Vector2(3, 6), 3, Direction.Right)
        };

        return (goal, map, expected);
    }
}