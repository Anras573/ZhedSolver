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
        var map = new Dictionary<Vector2, int>();
        var goal = new Vector2(-1, -1);

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (char.IsDigit(input[y][x]))
                    map.Add(new Vector2(x, y), int.Parse(input[y][x].ToString()));
                
                // We assume there's only one goal!
                if (input[y][x] == 'x')
                    goal = new Vector2(x, y);
            }
        }
        
        return new Solver(map, goal);
    }
}