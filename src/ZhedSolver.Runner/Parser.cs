using System.Numerics;

namespace ZhedSolver.Runner;

public static class Parser
{
    public static ISolver Parse(string input)
    {
        return Parse(input.Split(Environment.NewLine));
    }
    
    public static ISolver Parse(string[] input)
    {
        var map = new Dictionary<Vector2, char>();

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                map.Add(new Vector2(x, y), input[y][x]);
            }
        }
        
        return new Solver();
    }
}