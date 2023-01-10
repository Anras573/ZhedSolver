﻿using ZhedSolver.Runner.Models;

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
        var (minX, minY, maxX, maxY) = (int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (char.IsDigit(input[y][x]))
                {
                    map.Add(new Vector2(x, y), int.Parse(input[y][x].ToString()));

                    if (x < minX)
                        minX = x;
                    else if (x > maxX)
                        maxX = x;

                    if (y < minY)
                        minY = y;
                    else if (y > maxY)
                        maxY = y;
                }
                
                // We assume there's only one goal!
                if (input[y][x] == 'x')
                    goal = new Vector2(x, y);
            }
        }
        
        return new Solver(map, goal, new Bounds(new Vector2(minX, minY), new Vector2(maxX, maxY)));
    }
}