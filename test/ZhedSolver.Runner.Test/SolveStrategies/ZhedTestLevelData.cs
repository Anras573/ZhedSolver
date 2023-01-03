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
}