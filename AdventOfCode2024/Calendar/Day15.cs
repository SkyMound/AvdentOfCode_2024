using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Security;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Calendar;

public class Day15 : IDayProblem
{
    public static int Day => 15;

    public static string SolvePart1(string input)
    {
        HashSet<Coordinate> walls = [];
        HashSet<Coordinate> boxes = [];
        Coordinate robot = new(-1,-1);
        string[] lines = input.Split("\n\r")[0].Split('\n');
        for(int row = 0; row < lines.Length; ++row)
            for(int col = 0; col < lines[row].Length; ++col)
            {
                if (lines[row][col] == '#')
                    walls.Add(new(row,col));
                if (lines[row][col] == 'O')
                    boxes.Add(new(row,col));
                if (lines[row][col] == '@')
                    robot = new(row,col);
            }
        Coordinate[] directions = input.Split("\n\r")[1].Where(Direction.ContainsKey).Select(symbol => Direction[symbol]).ToArray();

        foreach (var offset in directions)
        {
            Coordinate nextRobotPosition = robot + offset;

            if (walls.Contains(nextRobotPosition))
                continue;
            
            if (boxes.Contains(nextRobotPosition))
                if (!TryToPush(nextRobotPosition, offset, walls, boxes))
                    continue;

            robot = nextRobotPosition;
        }
        
        return boxes.Sum(box => 100*box.Row + box.Col).ToString();
    }

    private static bool TryToPush(Coordinate position, Coordinate direction, HashSet<Coordinate> walls, HashSet<Coordinate> boxes)
    {
        Coordinate nextBoxPosition = position + direction;

        if (walls.Contains(nextBoxPosition))
            return false;
            
        if (boxes.Contains(nextBoxPosition))
            if (!TryToPush(nextBoxPosition, direction, walls, boxes))
                return false;

        boxes.Remove(position);
        boxes.Add(nextBoxPosition);
        return true;
    }

    public static string SolvePart2(string input)
    {
        HashSet<Coordinate> walls = [];
        Dictionary<Coordinate,BoxPart> boxes = [];
        Coordinate robot = new(-1,-1);
        string[] lines = input.Split("\n\r")[0].Split('\n');
        for(int row = 0; row < lines.Length; ++row)
            for(int col = 0; col < lines[row].Length; ++col)
            {
                if (lines[row][col] == '#')
                {
                    walls.Add(new(row,col*2));
                    walls.Add(new(row,col*2+1));
                }
                if (lines[row][col] == 'O')
                {
                    BoxPart leftPart = new(new(0,0));
                    BoxPart rightPart = new(new(0,1));
                    Box box = new([leftPart, rightPart]);
                    boxes.Add(new(row,col*2), leftPart);
                    boxes.Add(new(row,col*2+1), rightPart);
                }
                if (lines[row][col] == '@')
                    robot = new(row,col*2);
            }
        Coordinate[] directions = input.Split("\n\r")[1].Where(Direction.ContainsKey).Select(symbol => Direction[symbol]).ToArray();
        foreach (var offset in directions)
        {
            Coordinate nextRobotPosition = robot + offset;
            if (CanMove(nextRobotPosition, offset, walls, boxes))
            {
                ApplyBoxMove(nextRobotPosition, offset, boxes, []);
                robot = nextRobotPosition;
            }
        }
        Coordinate left = new(0,0);
        return boxes.Where(box=>box.Value.localPosition.Equals(left)).Sum(box => 100*box.Key.Row + box.Key.Col).ToString();
    }

    private static bool CanMove(Coordinate position, Coordinate direction, HashSet<Coordinate> walls, Dictionary<Coordinate,BoxPart> boxes)
    {
        if (walls.Contains(position))
            return false;
        
        if (boxes.TryGetValue(position, out var partInFront))
        {
            bool pushable = true;
            foreach(Coordinate otherCollision in partInFront.GetNeighborCoordinates(direction, position))
                pushable &= CanMove(otherCollision, direction, walls, boxes);
            return pushable;
        }

        return true;
    }

    private static void ApplyBoxMove(Coordinate position, Coordinate direction, Dictionary<Coordinate,BoxPart> boxes, HashSet<Box> boxMoved)
    {
        if (boxes.TryGetValue(position, out var partFocused))
        {
            if (!boxMoved.Contains(partFocused.Parent))
            {
                boxMoved.Add(partFocused.Parent);

                foreach(Coordinate otherCollision in partFocused.GetNeighborCoordinates(direction, position))
                    ApplyBoxMove(otherCollision, direction, boxes, boxMoved);

                foreach(BoxPart part in partFocused.Parent.Parts)
                    boxes.Remove(position - partFocused.localPosition + part.localPosition);
                foreach(BoxPart part in partFocused.Parent.Parts)
                    boxes.Add(position - partFocused.localPosition + part.localPosition + direction, part);
            }
        }
    }

    private static readonly Dictionary<char,Coordinate> Direction = new()
    {
        {'>', new(0,1)},
        {'v', new(1,0)},
        {'<', new(0,-1)},
        {'^', new(-1,0)}
    };

    class Box 
    {
        public List<BoxPart> Parts;

        public Box(BoxPart[] parts)
        {
            Parts = [];
            foreach(var part in parts)
            {
                part.Parent = this;
                Parts.Add(part);
            }
        }

        public List<Coordinate> GetNeighborCoordinates(Coordinate direction)
        {
            HashSet<Coordinate> neighbors = [];
            HashSet<Coordinate> piece = [];
            foreach(BoxPart part in Parts)
            {
                piece.Add(part.localPosition);
                neighbors.Remove(part.localPosition);
                if (!piece.Contains(part.localPosition + direction))
                    neighbors.Add(part.localPosition + direction);
            }
            return neighbors.ToList();
        }
    }

    class BoxPart
    {
        public Coordinate localPosition;
        public Box Parent;

        public BoxPart(Coordinate pos)
        {
            localPosition = pos;
            Parent = null;
        }

        public IEnumerable<Coordinate> GetNeighborCoordinates(Coordinate direction, Coordinate at)
        {
            foreach(Coordinate neighbor in Parent.GetNeighborCoordinates(direction))
                yield return neighbor - localPosition + at;
        }
    }
}
