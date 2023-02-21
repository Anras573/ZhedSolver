using System.Collections;

namespace ZhedSolver.Runner.Test.TestData;

public class Level16TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var goal = new Vector2(2, 5);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(4, 1), 1 },
            { new Vector2(6, 2), 1 },
            { new Vector2(1, 3), 2 },
            { new Vector2(2, 3), 1 },
            { new Vector2(5, 4), 1 },
            { new Vector2(5, 5), 1 },
            { new Vector2(3, 6), 3 }
        };
        
        var bounds = new Bounds(new Vector2(1, 1), new Vector2(6, 6));

        var expectedList = new List<List<Step>>
        {
            new List<Step>
            {
                new (new Vector2(3, 6), 3, Direction.Up),
                new (new Vector2(1, 3), 2, Direction.Right),
                new (new Vector2(5, 5), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left),
                new (new Vector2(4, 1), 1, Direction.Down),
                new (new Vector2(5, 4), 1, Direction.Left),
                new (new Vector2(2, 3), 1, Direction.Down)
            }
        };


        yield return new object[] { goal, map, expectedList, bounds };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}