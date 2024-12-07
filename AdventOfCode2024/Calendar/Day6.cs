
namespace AdventOfCode2024.Calendar;

public class Day6 : IDayProblem
{
    static int IDayProblem.Day => 6;

    public static string SolvePart1(string input)
    {
        int labWidth = input.IndexOf('\n') - 1;
        string labMap = input.Replace("\n", "").Replace("\r", "");
        int currentGuardPosition = labMap.IndexOf('^');
        int currentFacingDirection = 0;
        
        int[] forwardStepDirectionDependent = [-labWidth, 1, labWidth, -1]; // up, right, down, left
        HashSet<int> visitedPositions = [currentGuardPosition];
        bool isOutOfLab = false;

        while(!isOutOfLab)
        {
            int nextGuardPosition = currentGuardPosition + forwardStepDirectionDependent[currentFacingDirection];

            int currentRow = currentGuardPosition/labWidth;
            int nextRow = nextGuardPosition/labWidth;

            if (nextGuardPosition < 0 || nextGuardPosition >= labMap.Length)
                isOutOfLab = true;

            if (currentFacingDirection == 1 || currentFacingDirection == 3)
                if(currentRow != nextRow)
                    isOutOfLab = true;
            
            if(!isOutOfLab)
            {
                if(labMap[nextGuardPosition] == '#')
                    currentFacingDirection = (currentFacingDirection + 1)%4;
                else
                    currentGuardPosition = nextGuardPosition;
                
                visitedPositions.Add(currentGuardPosition);
            }
        }

        return visitedPositions.Count.ToString();
    }

    public static string SolvePart2(string input)
    {
        int labWidth = input.IndexOf('\n') - 1;
        string labMap = input.Replace("\n", "").Replace("\r", "");
        int startingGuardPosition = labMap.IndexOf('^');
        int startingFacingDirection = 0;
        int[] forwardStepDirectionDependent = [-labWidth, 1, labWidth, -1]; // up, right, down, left

        Dictionary<int,char> directionSymbol = new()
        {
            {0, '^'},
            {1, '>'},
            {2, 'v'},
            {3, '<'}
        };
        int currentGuardPosition = startingGuardPosition;
        int currentFacingDirection = startingFacingDirection;
        HashSet<int> visitedPositions = [currentGuardPosition];
        bool isOutOfLab = false;

        while(!isOutOfLab)
        {
            int nextGuardPosition = currentGuardPosition + forwardStepDirectionDependent[currentFacingDirection];

            int currentRow = currentGuardPosition/labWidth;
            int nextRow = nextGuardPosition/labWidth;

            if (nextGuardPosition < 0 || nextGuardPosition >= labMap.Length)
                isOutOfLab = true;

            if (currentFacingDirection == 1 || currentFacingDirection == 3)
                if(currentRow != nextRow)
                    isOutOfLab = true;
            
            if(!isOutOfLab)
            {
                if(labMap[nextGuardPosition] == '#')
                    currentFacingDirection = (currentFacingDirection + 1)%4;
                else
                {
                    currentGuardPosition = nextGuardPosition;
                }
                
                visitedPositions.Add(currentGuardPosition);
            }
        }

        int numberValidObstruction = 0;
        List<int> possibleObstructions = visitedPositions.ToList();
        possibleObstructions.Remove(startingGuardPosition);
        
        foreach(int obstructionPosition in possibleObstructions)
        {
            int currentGuardPosition2 = startingGuardPosition;
            int currentFacingDirection2 = startingFacingDirection;

            char[] labMapChar = labMap.ToCharArray(); 
            labMapChar[obstructionPosition] = '#';
            bool isOutOfLab2 = false;

            while(!isOutOfLab2)
            {
                int nextGuardPosition = currentGuardPosition2 + forwardStepDirectionDependent[currentFacingDirection2];

                if (nextGuardPosition < 0 || nextGuardPosition >= labMap.Length)
                    isOutOfLab2 = true;
                
                if (currentFacingDirection2 == 1 && currentGuardPosition2%labWidth==0)
                    isOutOfLab2 = true;

                if (currentFacingDirection2 == 3 && currentGuardPosition2%labWidth==labWidth-1)
                    isOutOfLab2 = true;

                if(!isOutOfLab2)
                {
                    if(labMapChar[nextGuardPosition] == '#')
                    {
                        currentFacingDirection2 = (currentFacingDirection2 + 1)%4;
                    }
                    else
                    {
                        if(labMapChar[currentGuardPosition2] == (char)currentFacingDirection2)
                        {
                            ++numberValidObstruction;
                            break;
                        }

                        labMapChar[currentGuardPosition2] = (char)currentFacingDirection2;
                        currentGuardPosition2 = nextGuardPosition;
                    }
                }
            }
        }

        return numberValidObstruction.ToString();
    }
}