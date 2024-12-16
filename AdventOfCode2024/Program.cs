// See https://aka.ms/new-console-template for more information
using AdventOfCode2024;
using AdventOfCode2024.Calendar;
using AdventOfCode2024.Common;
using static AdventOfCode2024.SolutionRunner;

class Program
{
    static void Main()
    {
        Coordinate test = new(0,0);

        Run<Day15>(Part._1);
        // Run<Day13>(Part._2);
    }
}
