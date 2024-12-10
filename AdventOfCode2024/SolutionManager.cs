using System;
using AdventOfCode2024.Calendar;

namespace AdventOfCode2024;

public class SolutionManager
{
    private static readonly Dictionary<(int,int), Func<string, string>> _problems = new();

    static SolutionManager()
    {
        Register<Day1>();
        Register<Day2>();
        Register<Day3>();
        Register<Day4>();
        Register<Day5>();
        Register<Day6>();
        Register<Day7>();
        Register<Day8>();
        Register<Day9>();
        Register<Day10>();
    }

    private static void Register<T>() where T : IDayProblem
    {
        _problems[(T.Day,1)] = T.SolvePart1;
        _problems[(T.Day,2)] = T.SolvePart2;
    }

    public static string Run(int day, int part, string inputFileName = "")
    {
        if (!_problems.TryGetValue((day, part), out var problem))
        {
            Console.WriteLine($"No problem registered for day {day}.");
            return string.Empty;
        }

        string path = "Inputs\\"+ (string.IsNullOrEmpty(inputFileName) ? day.ToString() : inputFileName) +".txt";

        if(!File.Exists(path))
        {
            Console.WriteLine("No input found here : " + path);
            return string.Empty;
        }

        string inputData = File.ReadAllText(path); 

        Console.WriteLine($"Running Day {day} Part {part}");

        DateTime startTime = DateTime.Now;
        
        string result = problem(inputData);
        Console.WriteLine(result);
        
        TimeSpan executionTime = DateTime.Now - startTime;

        Console.WriteLine("Took " + executionTime.ToString());

        return result;
    }
}
