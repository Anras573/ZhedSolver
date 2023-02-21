using ZhedSolver.Runner.Helpers;

namespace ZhedSolver.Runner.Test.Helpers;

public class PerrmutationsHelperTest
{
    [Fact]
    public void GivenList_WhenCalled_ThenDoNotMutateList()
    {
        var list = new List<Vector2>
        {
            new (1, 1),
            new (2, 2),
            new (3, 3)
        };
        
        PermutationsHelper.HeapPermute(list, list.Count, new List<List<Vector2>>(), _ => true);
        
        var expected = new List<Vector2>
        {
            new (1, 1),
            new (2, 2),
            new (3, 3)
        };
        
        Assert.NotEmpty(list);
        Assert.Equal(expected, list);
    }
    
    [Fact]
    public void GivenList_WhenCalled_ThenGetPermutations()
    {
        var list = new List<Vector2>
        {
            new (1, 1),
            new (2, 2),
            new (3, 3)
        };

        var permutations = new List<List<Vector2>>();

        PermutationsHelper.HeapPermute(list, list.Count, permutations, _ => true);
        
        var expected = new List<List<Vector2>>
        {
            new List<Vector2>
            {
                new (1, 1),
                new (2, 2),
                new (3, 3)
            },
            new List<Vector2>
            {
                new (1, 1),
                new (3, 3),
                new (2, 2)
            },
            new List<Vector2>
            {
                new (2, 2),
                new (1, 1),
                new (3, 3)
            },
            new List<Vector2>
            {
                new (2, 2),
                new (3, 3),
                new (1, 1)
            },
            new List<Vector2>
            {
                new (3, 3),
                new (1, 1),
                new (2, 2)
            },
            new List<Vector2>
            {
                new (3, 3),
                new (2, 2),
                new (1, 1)
            }
        };
        
        Assert.NotEmpty(permutations);
        Assert.Equal(expected.Count, permutations.Count);

        foreach (var permutation in permutations)
        {
            Assert.Contains(permutation, expected);
        }
        
        foreach (var permutation in expected)
        {
            Assert.Contains(permutation, permutations);
        }
    }
    
    [Fact]
    public void GivenList_WhenCalledWithPredicate_ThenGetPermutationsMatchingPredicate()
    {
        var list = new List<Vector2>
        {
            new (1, 1),
            new (2, 2),
            new (3, 3)
        };

        var permutations = new List<List<Vector2>>();

        PermutationsHelper.HeapPermute(list, list.Count, permutations, p => p[^1] == new Vector2(3, 3));
        
        var expected = new List<List<Vector2>>
        {
            new List<Vector2>
            {
                new (1, 1),
                new (2, 2),
                new (3, 3)
            },
            new List<Vector2>
            {
                new (2, 2),
                new (1, 1),
                new (3, 3)
            }
        };
        
        Assert.NotEmpty(permutations);
        Assert.Equal(expected.Count, permutations.Count);

        foreach (var permutation in permutations)
        {
            Assert.Contains(permutation, expected);
        }
        
        foreach (var permutation in expected)
        {
            Assert.Contains(permutation, permutations);
        }
    }
}