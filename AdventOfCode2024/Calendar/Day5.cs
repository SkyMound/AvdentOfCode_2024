using System;

namespace AdventOfCode2024.Calendar;

public class Day5 : IDayProblem
{
    public static int Day => 5;

    public static string SolvePart1(string input)
    {
        Dictionary<int,HashSet<int>> NeedToBeBefore = []; // Store the pages that need to be before a given one
        List<int[]> updates = [];

        string[] lines = input.Split('\n');
        bool processingRules = true;
        foreach(string line in lines)
        {
            if (processingRules)
            {
                if (!line.Contains('|'))
                {
                    processingRules = false;
                    continue;
                }

                int[] numbers = line.Split('|').Select(int.Parse).ToArray();
                if (NeedToBeBefore.TryGetValue(numbers[1], out HashSet<int> set))
                    set.Add(numbers[0]);
                else
                    NeedToBeBefore.Add(numbers[1],[numbers[0]]);
            }
            else
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
        }

        int middlePagesSum = 0;

        foreach(int[] update in updates)
        {
            bool safeFlag = true;
            HashSet<int> alreadyPrintedPages = [];
            
            for(int i = 0; i < update.Length; ++i)
            {
                if (!safeFlag)
                    break;
                
                for (int j = i + 1; j < update.Length; ++j)
                {
                    int pageBefore = update[i];
                    int pageAfter = update[j];
                    if (NeedToBeBefore.ContainsKey(pageBefore) && NeedToBeBefore[pageBefore].Contains(pageAfter))
                    {
                        safeFlag = false;
                        break;
                    }
                }
            }

            if (safeFlag)
                middlePagesSum += update[update.Length/2];
        }

        return middlePagesSum.ToString();
    }

    public static string SolvePart2(string input)
    {
                Dictionary<int,HashSet<int>> NeedToBeBefore = []; // Store the pages that need to be before a given one
        List<int[]> updates = [];

        string[] lines = input.Split('\n');
        bool processingRules = true;
        foreach(string line in lines)
        {
            if (processingRules)
            {
                if (!line.Contains('|'))
                {
                    processingRules = false;
                    continue;
                }

                int[] numbers = line.Split('|').Select(int.Parse).ToArray();
                if (NeedToBeBefore.TryGetValue(numbers[1], out HashSet<int> set))
                    set.Add(numbers[0]);
                else
                    NeedToBeBefore.Add(numbers[1],[numbers[0]]);
            }
            else
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
        }

        int middlePagesSum = 0;

        foreach(int[] update in updates)
        {
            bool safeFlag = true;
            HashSet<int> alreadyPrintedPages = [];
            
            for(int i = 0; i < update.Length; ++i)
            {
                for (int j = i + 1; j < update.Length; ++j)
                {
                    int pageBefore = update[i];
                    int pageAfter = update[j];
                    if (NeedToBeBefore.ContainsKey(pageBefore) && NeedToBeBefore[pageBefore].Contains(pageAfter))
                    {
                        int temp = update[i];
                        update[i] = update[j];
                        update[j] = temp;
                        safeFlag = false;
                    }
                }
            }

            if (!safeFlag)
                middlePagesSum += update[update.Length/2];
        }

        return middlePagesSum.ToString();
    }
}
