namespace ZhedSolver.Runner.Test;

public class ParserTest
{
    [Fact] // x: 4 y: 3
    public void CanParseInputAsString()
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

        Assert.NotNull(result);
    }
    
    [Fact]
    public void CanParseInputAsStringArray()
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

        Assert.NotNull(result);
    }
}