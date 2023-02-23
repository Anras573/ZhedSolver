using System.Collections;

namespace ZhedSolver.Runner.Test.TestData;

public class Level29TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var goal = new Vector2(4, 2);
        var map = new Dictionary<Vector2, int>
        {
            { new Vector2(2, 1), 2 },
            { new Vector2(6, 1), 1 },
            { new Vector2(0, 2), 1 },
            { new Vector2(6, 2), 1 },
            { new Vector2(2, 3), 1 },
            { new Vector2(7, 3), 1 },
            { new Vector2(1, 4), 2 },
            { new Vector2(1, 5), 1 },
            { new Vector2(4, 5), 1 },
            { new Vector2(6, 6), 3 },
            { new Vector2(3, 7), 1 },
            { new Vector2(5, 7), 1 }
        };
        
        var bounds = new Bounds(new Vector2(0, 1), new Vector2(7, 7));

        var expectedList = new List<List<Step>>
        {
            new List<Step>
            {
                new (new Vector2(6, 6), 3, Direction.Left),
                new (new Vector2(4, 5), 1, Direction.Left),
                new (new Vector2(3, 7), 1, Direction.Up),
                new (new Vector2(6, 1), 1, Direction.Down),
                new (new Vector2(2, 3), 1, Direction.Down),
                new (new Vector2(7, 3), 1, Direction.Left),
                new (new Vector2(1, 4), 2, Direction.Right),
                new (new Vector2(0, 2), 1, Direction.Right),
                new (new Vector2(2, 1), 2, Direction.Down),
                new (new Vector2(1, 5), 1, Direction.Right),
                new (new Vector2(5, 7), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left)
            },
            new List<Step>
            {
                new (new Vector2(2, 3), 1, Direction.Down),
                new (new Vector2(0, 2), 1, Direction.Right),
                new (new Vector2(6, 1), 1, Direction.Down),
                new (new Vector2(4, 5), 1, Direction.Right),
                new (new Vector2(7, 3), 1, Direction.Left),
                new (new Vector2(2, 1), 2, Direction.Down),
                new (new Vector2(6, 6), 3, Direction.Left),
                new (new Vector2(1, 5), 1, Direction.Right),
                new (new Vector2(3, 7), 1, Direction.Up),
                new (new Vector2(1, 4), 2, Direction.Right),
                new (new Vector2(5, 7), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left)
            },
            new List<Step>
            {
                new (new Vector2(0, 2), 1, Direction.Right),
                new (new Vector2(4, 5), 1, Direction.Up),
                new (new Vector2(2, 1), 2, Direction.Down),
                new (new Vector2(6, 6), 3, Direction.Left),
                new (new Vector2(1, 4), 2, Direction.Right),
                new (new Vector2(3, 7), 1, Direction.Up),
                new (new Vector2(2, 3), 1, Direction.Down),
                new (new Vector2(6, 1), 1, Direction.Down),
                new (new Vector2(1, 5), 1, Direction.Right),
                new (new Vector2(7, 3), 1, Direction.Left),
                new (new Vector2(5, 7), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left)
            },
            new List<Step>
            {
                new (new Vector2(0, 2), 1, Direction.Right),
                new (new Vector2(2, 3), 1, Direction.Down),
                new (new Vector2(2, 1), 2, Direction.Down),
                new (new Vector2(6, 6), 3, Direction.Left),
                new (new Vector2(1, 5), 1, Direction.Right),
                new (new Vector2(3, 7), 1, Direction.Up),
                new (new Vector2(1, 4), 2, Direction.Right),
                new (new Vector2(6, 1), 1, Direction.Down),
                new (new Vector2(4, 5), 1, Direction.Right),
                new (new Vector2(7, 3), 1, Direction.Left),
                new (new Vector2(5, 7), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left)
            },
            new List<Step>
            {
                new (new Vector2(2, 3), 1, Direction.Down),
                new (new Vector2(4, 5), 1, Direction.Up),
                new (new Vector2(0, 2), 1, Direction.Right),
                new (new Vector2(6, 1), 1, Direction.Down),
                new (new Vector2(1, 4), 2, Direction.Right),
                new (new Vector2(7, 3), 1, Direction.Left),
                new (new Vector2(6, 6), 3, Direction.Left),
                new (new Vector2(2, 1), 2, Direction.Down),
                new (new Vector2(3, 7), 1, Direction.Up),
                new (new Vector2(1, 5), 1, Direction.Right),
                new (new Vector2(5, 7), 1, Direction.Up),
                new (new Vector2(6, 2), 1, Direction.Left)
            }
        };


        yield return new object[] { goal, map, expectedList, bounds };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}