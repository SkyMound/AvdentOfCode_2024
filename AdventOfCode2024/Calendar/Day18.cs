using System;
using System.Collections;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Calendar;

public class Day18 : IDayProblem
{
    public static int Day => 18;

    public static string SolvePart1(string input)
    {
        int width = 7, height = 7;
        Coordinate position = new(0,0);
        Coordinate target = new(width,height);
        HashSet<Coordinate> bytes = [];
        Stack<Coordinate> bytesAboutToFall = new (input.Split("\r\n").Select(str => new Coordinate(int.Parse(str.Split(',')[0]), int.Parse(str.Split(',')[1]))).Reverse());

        foreach(Coordinate byt in bytesAboutToFall)
        {
            Console.WriteLine(byt);
        }

        return string.Empty;
    }



    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    // private static int GetStepRequired(Coordinate position, Coordinate target, HashSet<Coordinate> bytes, int width, int height, Stack<Coordinate> bytesAboutToFall)
    // {
    //     if (position == target)
    //         return 0;
        
    //     int minStep = int.MaxValue;
    //     foreach (Coordinate nextPosition in position.GetOrthogonals())
    //     {
    //         if (!bytes.Contains(nextPosition) && nextPosition.Row > 0 && nextPosition.Col > 0 && nextPosition.Row < width && nextPosition.Col < height)
    //             minStep = Math.Min(minStep,GetStepRequired(nextPosition, target, bytes, width, height, bytesAboutToFall) + 1);
    //     }
    // }
}
