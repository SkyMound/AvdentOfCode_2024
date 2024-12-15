using System;
using System.Linq;

namespace AdventOfCode2024.Calendar;

public class Day15 : IDayProblem
{
    public static int Day => 15;

    public static string SolvePart1(string input)
    {
        HashSet<(int,int)> walls = [];
        HashSet<(int row,int col)> boxes = [];
        (int row, int col) robotPosition = (-1,-1);
        string[] lines = input.Split("\n\r")[0].Split('\n');
        for(int row = 0; row < lines.Length; ++row)
            for(int col = 0; col < lines[row].Length; ++col)
            {
                if (lines[row][col] == '#')
                    walls.Add((row,col));
                if (lines[row][col] == 'O')
                    boxes.Add((row,col));
                if (lines[row][col] == '@')
                    robotPosition = (row,col);
            }
        (int,int)[] directions = input.Split("\n\r")[1].Where(Direction.ContainsKey).Select(symbol => Direction[symbol]).ToArray();

        foreach ((int rowOffset, int colOffset) in directions)
        {
            (int,int) nextRobotPosition = (robotPosition.row + rowOffset, robotPosition.col + colOffset);

            if (walls.Contains(nextRobotPosition))
                continue;
            
            if (boxes.Contains(nextRobotPosition))
                if (!TryToPush(nextRobotPosition, (rowOffset,colOffset), walls, boxes))
                    continue;

            robotPosition = nextRobotPosition;
        }
        
        return boxes.Sum(box => 100*box.row + box.col).ToString();
    }

    private static bool TryToPush((int row, int col) position, (int row, int col) direction, HashSet<(int,int)> walls, HashSet<(int,int)> boxes)
    {
        (int,int) nextBoxPosition = (position.row + direction.row, position.col + direction.col);

        if (walls.Contains(nextBoxPosition))
            return false;
            
        if (boxes.Contains(nextBoxPosition))
            if (!TryToPush(nextBoxPosition, direction, walls, boxes))
                return false;

        boxes.Remove(position);
        boxes.Add(nextBoxPosition);
        return true;
    }

        private static bool TryToPushBigBox((int row, int col) position, (int row, int col) direction, HashSet<(int,int)> walls, HashSet<(int,int)> boxes)
    {
        (int,int) nextBoxPosition = (position.row + direction.row, position.col + direction.col);

        if (walls.Contains(nextBoxPosition))
            return false;
            
        if (boxes.Contains(nextBoxPosition))
            if (!TryToPushBigBox(nextBoxPosition, direction, walls, boxes))
                return false;

        boxes.Remove(position);
        boxes.Add(nextBoxPosition);
        return true;
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<char,(int,int)> Direction = new()
    {
        {'>', (0,1)},
        {'v', (1,0)},
        {'<', (0,-1)},
        {'^', (-1,0)}
    };

    struct BigBox
    {
        
    }    
}
