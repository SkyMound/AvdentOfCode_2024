using System;

namespace AdventOfCode2024.Calendar;

public class Day11 : IDayProblem
{
    public static int Day => 11;

    public static string SolvePart1(string input) => CountStoneAfterBlink(GetStones(input), 25).ToString();

    public static string SolvePart2(string input) => CountStoneAfterBlink(GetStones(input), 75).ToString();

    private static IEnumerable<long> BlinkStone(long stone)
    {
        // Rule 1
        if (stone == 0)
        {
            yield return 1;
            yield break;
        }
        
        // Rule 2
        int digits = (int)Math.Log10(stone) + 1;
        if (digits % 2 == 0)
        {
            int halfLength = digits / 2;

            long divisor = (int)Math.Pow(10, halfLength);
            
            long leftHalf = stone / divisor;
            long rightHalf = stone % divisor;
        
            yield return leftHalf;
            yield return rightHalf;
            yield break;
        }

        // Rule 3
        yield return stone * 2024;
    }

    private static Dictionary<long,long> GetStones(string input) => input.Split(' ').GroupBy(x => x).ToDictionary(g => long.Parse(g.Key), g => (long)g.Count());
    
    private static long CountStoneAfterBlink(Dictionary<long,long> stones, int blinks)
    {
        for(int _ = 0; _ < blinks; ++_)
        {
            Dictionary<long,long> blinkedCounterMap = [];

            foreach(var stoneEntry in stones)
                foreach(long newStone in BlinkStone(stoneEntry.Key))
                    if (blinkedCounterMap.ContainsKey(newStone))
                        blinkedCounterMap[newStone] += stoneEntry.Value;
                    else
                        blinkedCounterMap[newStone] = stoneEntry.Value;
            
            stones = blinkedCounterMap;
        }
        
        return stones.Sum(kv => kv.Value);
    }
}