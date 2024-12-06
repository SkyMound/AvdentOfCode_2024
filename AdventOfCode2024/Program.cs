// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Calendar;
using AdventOfCode2024.Calendar._1;
using AdventOfCode2024.Calendar._6;

class Program
{
    static async Task Main(string[] args)
    {
        AdventDay daySolver = new Day6Part2();
        await daySolver.Run();
    }
}