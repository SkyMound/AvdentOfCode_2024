using System;

namespace AdventOfCode2024.Calendar;

public class Day2 : IDayProblem
{
    public static int Day => 2;

    public static string SolvePart1(string input)
    {
        int[][] reports = input.Split("\n").Select(str => str.Split(" ").Select(int.Parse).ToArray()).ToArray();

        int safeNumber = 0;

        foreach(int[] report in reports)
        {
            if(report.Length < 2)
            {
                ++safeNumber;
                continue;
            }

            int sign = report[0] - report[1] < 0 ? 1 : -1;
            bool safeFlag = true;
            for(int levelIndex = 1; levelIndex < report.Length; ++levelIndex)
            {
                int adjacentDifference = report[levelIndex -1 ] - report[levelIndex];
                if (!(adjacentDifference * sign > 0 && adjacentDifference * sign < 4))
                {
                    Console.WriteLine(adjacentDifference);
                    safeFlag = false;
                    break;
                }
            }

            if(safeFlag)
                ++safeNumber;

        }
        return safeNumber.ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}