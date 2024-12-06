using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;

namespace AdventOfCode2024.Calendar._6;

public class Day6 : AdventDay
{
    public override int DayNumber => 6;


    protected override string InternalRun(string input)
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

}
