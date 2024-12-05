namespace AdventOfCode2024.Calendar._1;

public class Day1Part2 : AdventDay
{
    public override int DayNumber { get => 1; }

    protected override string InternalRun(string input)
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
    
}
