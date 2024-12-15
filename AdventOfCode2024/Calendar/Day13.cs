using System;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Calendar;

public class Day13 : IDayProblem
{
    public static int Day => 13;

    public struct ClawMachine
    {
        public long buttonA_X;
        public long buttonA_Y;
        public long buttonB_X;
        public long buttonB_Y;
        public long prize_X;
        public long prize_Y;
    }

    private static IEnumerable<ClawMachine> GetMachines(string input)
    {
        Regex regex = new Regex(@"\d+");

        foreach(string strMachine in input.Split("\n\r"))
        {
            MatchCollection matches = regex.Matches(strMachine);
            ClawMachine mac = new();
            mac.buttonA_X = int.Parse(matches[0].Value);
            mac.buttonA_Y = int.Parse(matches[1].Value);
            mac.buttonB_X = int.Parse(matches[2].Value);
            mac.buttonB_Y = int.Parse(matches[3].Value);
            mac.prize_X = int.Parse(matches[4].Value);
            mac.prize_Y = int.Parse(matches[5].Value);

            yield return mac;
        }
    }

    public static string SolvePart1(string input)
    {
        long tokenSpend = 0;

        foreach(ClawMachine m in GetMachines(input))
        {
            (long x, long y, bool found) = SolveEquationSystem(m.buttonA_X, m.buttonB_X, m.prize_X + 10000000000000, m.buttonA_Y, m.buttonB_Y, m.prize_Y + 10000000000000);

            if (found)
                tokenSpend += x * 3 + y;
        }
        return tokenSpend.ToString();
    }


    static (long, long, bool) SolveEquationSystem(long a1, long b1, long c1, long a2, long b2, long c2)
    {
        long det = a1 * b2 - a2 * b1;

        if (det == 0)
        {
            return (0, 0, false); // No unique solution
        }

        // Cramer's Rule
        long x = (c1 * b2 - c2 * b1) / det;
        long y = (a1 * c2 - a2 * c1) / det;

        // Check if integer solutions are valid
        if ((c1 == a1 * x + b1 * y) && (c2 == a2 * x + b2 * y))
        {
            return (x, y, true);
        }
        else
        {
            return (0, 0, false);
        }
    }

    public static string SolvePart2(string input)
    {
        long tokenSpend = 0;
        foreach(ClawMachine m in GetMachines(input))
        {
            (long x, long y, bool found) = SolveEquationSystem(m.buttonA_X, m.buttonB_X, m.prize_X + 10000000000000, m.buttonA_Y, m.buttonB_Y, m.prize_Y + 10000000000000);

            if (found)
                tokenSpend += x * 3 + y;
        }
        return tokenSpend.ToString();
    }
}
