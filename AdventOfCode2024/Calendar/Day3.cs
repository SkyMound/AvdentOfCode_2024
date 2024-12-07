using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Calendar;

public class Day3 : IDayProblem
{
    public static int Day => 3;

    public static string SolvePart1(string input)
    {
        string pattern = @"mul\(\d{1,3},\d{1,3}\)";

        Regex rg = new(pattern);

        MatchCollection matchedMultiplication = rg.Matches(input);
        
        int multiplicationSum = 0;

        foreach(Match match in matchedMultiplication)
        {
            Regex numberRegex = new(@"\d{1,3}");
            MatchCollection numbers = numberRegex.Matches(match.Value);
            multiplicationSum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }
        
        return multiplicationSum.ToString();
    }

    public static string SolvePart2(string input)
    {
        string pattern = @"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))";

        Regex rg = new(pattern);

        MatchCollection matchedMultiplication = rg.Matches(input);
        
        int multiplicationSum = 0;
        bool isEnabled = true;
        
        foreach(Match match in matchedMultiplication)
        {
            if (match.Value.Equals("do()"))
                isEnabled = true;
            else if (match.Value.Equals("don't()"))
                isEnabled = false;
            else if (isEnabled)
            {
                Regex numberRegex = new(@"\d{1,3}");
                MatchCollection numbers = numberRegex.Matches(match.Value);
                multiplicationSum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
            }
        }
        
        return multiplicationSum.ToString();
    }
}
