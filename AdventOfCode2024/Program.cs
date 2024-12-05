// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Calendar;
using AdventOfCode2024.Calendar._1;

class Program
{
    static async Task Main(string[] args)
    {
        AdventDay daySolver = new Day1();
        await daySolver.Run();
    }
}