namespace ZhedSolver.Runner;

public static class Parser
{
    public static bool Parse(string input)
    {
        return Parse(input.Split(Environment.NewLine));
    }
    public static bool Parse(string[] input)
    {
        return true;
    }
}