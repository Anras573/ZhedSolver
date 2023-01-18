using System.Numerics;
using ZhedSolver.Runner.Utils;

namespace ZhedSolver.Runner.Test.Utils;

public class EnumerableExtensionsTest
{
    [Fact]
    public void CallingPermutationsOnListReturnsPermutations()
    {
        var sut = new List<int> { 1, 2, 3 };

        var permutations = sut.GetPermutations(sut.Count);

        var listPermutations = permutations.Select(p => p.ToList()).ToList();

        var expectedList = new List<List<int>>
        {
            new() { 1, 2, 3 },
            new() { 1, 3, 2 },
            new() { 2, 1, 3 },
            new() { 2, 3, 1 },
            new() { 3, 1, 2 },
            new() { 3, 2, 1 }
        };
        
        Assert.Equal(expectedList, listPermutations);
    }
    
    [Fact]
    public void CallingPermutationsOnListWithObjectsReturnsPermutations()
    {
        var sut = new List<Vector2> { new (1, 1), new (2, 2), new (3, 3) };

        var permutations = sut.GetPermutations(sut.Count);

        var listPermutations = permutations.Select(p => p.ToList()).ToList();

        var expectedList = new List<List<Vector2>>
        {
            new() { new Vector2(1, 1), new Vector2(2, 2), new Vector2(3, 3) },
            new() { new Vector2(1, 1), new Vector2(3, 3), new Vector2(2, 2) },
            new() { new Vector2(2, 2), new Vector2(1, 1), new Vector2(3, 3) },
            new() { new Vector2(2, 2), new Vector2(3, 3), new Vector2(1, 1) },
            new() { new Vector2(3, 3), new Vector2(1, 1), new Vector2(2, 2) },
            new() { new Vector2(3, 3), new Vector2(2, 2), new Vector2(1, 1) }
        };
        
        Assert.Equal(expectedList, listPermutations);
    }
}