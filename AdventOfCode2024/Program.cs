// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Calendar;
using AdventOfCode2024.Calendar._1;
using AdventOfCode2024.Calendar._2;

class Program
{
    static async Task Main(string[] args)
    {
        AdventDay daySolver = new Day2();
        await daySolver.Run();
    }
}