
namespace AdventOfCode2024.Calendar._1;

public class Day1 : AdventDay
{
    public override int DayNumber { get => 1; }

    protected override string InternalRun(string input)
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

    private void SortedInsert(List<int> list, int valueToInsert)
    {
        int indexToAdd;
        for (indexToAdd = 0; indexToAdd < list.Count; ++indexToAdd)
            if (valueToInsert <= list[indexToAdd]) 
                break;
        
        list.Insert(indexToAdd, valueToInsert);
    }
}
