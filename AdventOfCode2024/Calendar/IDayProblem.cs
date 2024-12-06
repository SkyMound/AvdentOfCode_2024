using System;

namespace AdventOfCode2024.Calendar;

public interface IDayProblem
{
    public static abstract int Day { get; }
    abstract static string SolvePart1(string input);
    abstract static string SolvePart2(string input);
}
