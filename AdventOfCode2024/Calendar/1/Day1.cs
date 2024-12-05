using System;

namespace AdventOfCode2024.Calendar._1;

public class Day1 : AdventDay
{
    public override int DayNumber { get => 1; }

    protected override string InternalRun(InputHandler input)
    {
        Console.WriteLine(DayNumber.ToString());
        return string.Empty;
    }
}
