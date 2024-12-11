using System;
using AdventOfCode2024.Calendar;

namespace AdventOfCode2024;

public class SolutionRunner
{
    public enum Part
    {
        _1 = 1,
        _2 = 2
    }

    public static string Run<T>(Part part, string inputFileName = "") where T : IDayProblem
    {
        string path = "Inputs\\"+ (string.IsNullOrEmpty(inputFileName) ? T.Day.ToString() : inputFileName) +".txt";

        if(!File.Exists(path))
        {
            Console.WriteLine("No input found here : " + path);
            return string.Empty;
        }

        string inputData = File.ReadAllText(path); 

        Console.WriteLine($"Running Day {T.Day} Part {part.GetHashCode()}");

        DateTime startTime = DateTime.Now;
        
        string result = part == Part._1 ? T.SolvePart1(inputData) : T.SolvePart2(inputData);
        Console.WriteLine(result);
        
        TimeSpan executionTime = DateTime.Now - startTime;

        Console.WriteLine("Took " + executionTime.ToString());

        return result;
    }
}
