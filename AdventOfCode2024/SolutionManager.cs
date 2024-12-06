using System;
using AdventOfCode2024.Calendar;

namespace AdventOfCode2024;

public class SolutionManager
{
    private static readonly Dictionary<(int,int), Func<string, string>> _problems = new();

    static SolutionManager()
    {
        Register<Day1>();
        Register<Day6>();
    }

    private static void Register<T>() where T : IDayProblem
    {
        _problems[(T.Day,1)] = T.SolvePart1;
        _problems[(T.Day,2)] = T.SolvePart2;
    }

    public static string Run(int day, int part)
    {
        if (!_problems.TryGetValue((day, part), out var problem))
        {
            Console.WriteLine($"No problem registered for day {day}.");
            return string.Empty;
        }
        
        string path = "C:\\Users\\tfresard\\Root\\projects\\AvdentOfCode_2024\\AdventOfCode2024\\Inputs\\"+day.ToString()+".txt";

        if(!File.Exists(path))
        {
            Console.WriteLine("No input found here : " + path);
            return string.Empty;
        }

        string inputData = File.ReadAllText(path); 

        DateTime startTime = DateTime.Now;
        
        string result = problem(inputData);
        Console.WriteLine(result);
        
        TimeSpan executionTime = DateTime.Now - startTime;

        Console.WriteLine("Took " + executionTime.ToString());

        return result;
    }
}
