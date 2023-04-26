# ZhedSolver

This repository contains a solver for the game [Zhed](https://play.google.com/store/apps/details?id=com.groundcontrol.zhed). The solver is implemented in C# and uses a depth-first search algorithm to find a solution for a given level.

## How to Use

To use the solver, you need to provide it with a level to solve. You can do this by creating a mulitline string as a visual representation of the level.

Here is an example of how to use the solver:

```csharp
const string file =
"""
----------
----------
----------
----123---
---1------
---2------
---3-----x
----------
----------
----------
""";

Parser
    .Parse(file)
    .Solve(new ParallelPermutationStrategy());
```

The `Solve` method returns a list of `Step` objects that represent the sequence of moves needed to solve the level. Each `Step` object contains the position of the cell that was moved, the value of the cell, and the direction of the move.

## Limitations

The solver has some limitations:

It assumes that the input level is valid, i.e., it has at least one solution and can be solved using the rules of the game.
It does not guarantee that it will find the optimal solution for the level, i.e., the solution with the minimum number of moves.
It may not be able to solve very complex levels within a reasonable amount of time.
