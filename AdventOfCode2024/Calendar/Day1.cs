
namespace AdventOfCode2024.Calendar;


public class Day1 : IDayProblem
{
    public static int Day => 1;

    public static string SolvePart1(string input)
    {
        string[] lines = input.Split("\n",StringSplitOptions.RemoveEmptyEntries);

        List<int> leftNumbers = []; 
        List<int> rightNumbers = []; 
        
        foreach(string line in lines)
        {
            int[] numberLeftAndRight = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            SortedInsert(leftNumbers, numberLeftAndRight[0]);
            SortedInsert(rightNumbers, numberLeftAndRight[1]);
        }

        int distance = 0;

        for(int numberIndex = 0; numberIndex < lines.Length; ++numberIndex)
            distance += Math.Abs(leftNumbers[numberIndex] - rightNumbers[numberIndex]);

        return distance.ToString();
    }

    public static string SolvePart2(string input)
    {
        string[] lines = input.Split("\n",StringSplitOptions.RemoveEmptyEntries);

        List<int> leftNumbers = []; 
        Dictionary<int,int> rightNumbers = []; 
        
        foreach(string line in lines)
        {
            int[] numberLeftAndRight = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            leftNumbers.Add(numberLeftAndRight[0]);
            if(!rightNumbers.TryAdd(numberLeftAndRight[1], 1))
                rightNumbers[numberLeftAndRight[1]] += 1;
        }

        int similarityScore = 0;

        foreach(int leftNumber in leftNumbers)
            similarityScore += rightNumbers.TryGetValue(leftNumber, out int occurence) ? leftNumber * occurence : 0;
        
        return similarityScore.ToString();
    }

    private static void SortedInsert(List<int> list, int valueToInsert)
    {
        int indexToAdd;
        for (indexToAdd = 0; indexToAdd < list.Count; ++indexToAdd)
            if (valueToInsert <= list[indexToAdd]) 
                break;
        
        list.Insert(indexToAdd, valueToInsert);
    }
}