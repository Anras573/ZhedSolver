namespace ZhedSolver.Runner.Test;

public class ParserTest
{
    [Fact]
    public void SolverCanParseInputAsString()
    {
        const string input = """
--------
--------
--------
---1x---
--------
--------
--------
--------
""";

        var result = Parser.Parse(input.Split(Environment.NewLine));

        Assert.True(result);
    }
    
    [Fact]
    public void SolverCanParseInputAsStringArray()
    {
        var input = new[]
        {
            "--------",
            "--------",
            "--------",
            "---1x---",
            "--------",
            "--------",
            "--------",
            "--------"
        };

        var result = Parser.Parse(input);

        Assert.True(result);
    }
}