using System.Collections;

namespace ZhedSolver.Runner.Test.TestData;

public class Level14TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var goal = new Vector2(6, 4);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(3, 1), 2 },
            { new Vector2(4, 2), 1 },
            { new Vector2(2, 3), 1 },
            { new Vector2(3, 4), 1 },
            { new Vector2(4, 5), 1 },
            { new Vector2(4, 6), 1 },
            { new Vector2(6, 6), 1 },
            { new Vector2(7, 7), 1 },
            { new Vector2(5, 8), 1 }
        };
        
        var bounds = new Bounds(new Vector2(2, 1), new Vector2(7, 8));

        var expectedList = new List<List<Step>>
        {
            new List<Step>
            {
                new (new Vector2(4, 5), 1, Direction.Right),
                new (new Vector2(4, 6), 1, Direction.Right),
                new (new Vector2(3, 1), 2, Direction.Down),
                new (new Vector2(6, 6), 1, Direction.Down),
                new (new Vector2(2, 3), 1, Direction.Right),
                new (new Vector2(7, 7), 1, Direction.Left),
                new (new Vector2(4, 2), 1, Direction.Down),
                new (new Vector2(5, 8), 1, Direction.Up),
                new (new Vector2(3, 4), 1, Direction.Right)
            }
        };


        yield return new object[] { goal, map, expectedList, bounds };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}