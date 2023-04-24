using System.Collections;

namespace ZhedSolver.Runner.Test.TestData;

public class Level11TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
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
        
        var bounds = new Bounds(new Vector2(3, 3), new Vector2(9, 6));

        var expectedList = new List<List<Step>>
        {
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
            new List<Step>
            {
                new (new Vector2(6, 3), 3, Direction.Down),
                new (new Vector2(3, 4), 1, Direction.Right),
                new (new Vector2(3, 5), 2, Direction.Right),
                new (new Vector2(5, 3), 2, Direction.Down),
                new (new Vector2(4, 3), 1, Direction.Down),
                new (new Vector2(3, 6), 3, Direction.Right)
            },
        };


        yield return new object[] { goal, map, expectedList, bounds };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}