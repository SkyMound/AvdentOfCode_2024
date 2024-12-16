using System;
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
        HashSet<BoxPart> boxes = [];
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
                    Box box = new()
                    boxes.Add(new(row,col*2));
                    boxes.Add(new(row,col*2+1));
                }
                if (lines[row][col] == '@')
                    robot = new(row,col*2);
            }
        Coordinate[] directions = input.Split("\n\r")[1].Where(Direction.ContainsKey).Select(symbol => Direction[symbol]).ToArray();

    }

    private static bool TryToPushBox(Box boxToPush, Coordinate position, Coordinate direction, HashSet<Coordinate> walls, HashSet<Coordinate> boxes)
    {
        Coordinate nextBoxPosition = position + direction;

        if (walls.Contains(nextBoxPosition))
            return false;
            
        if (boxes.Contains(nextBoxPosition))
            if (!TryToPushBox(nextBoxPosition, direction, walls, boxes))
                return false;

        boxes.Remove(position);
        boxes.Add(nextBoxPosition);
        return true;
    }

    private static readonly Dictionary<char,Coordinate> Direction = new()
    {
        {'>', new(0,1)},
        {'v', new(1,0)},
        {'<', new(0,-1)},
        {'^', new(-1,0)}
    };

    // class Box 
    // {
    //     public HashSet<Coordinate> shape;

    //     public IEnumerable<Coordinate> GetNeighborCoordinate(Coordinate direction)
    //     {
    //         foreach(Coordinate coord in shape)
    //         {
    //             Coordinate neighborPos = coord + direction;
    //             if (!shape.Contains(neighborPos))
    //                 yield return neighborPos;

    //         }
    //     }
    // }

    // class Box
    // {
    //     public Coordinate position;
    //     public List<Box> linkedTo;

    //     public IEnumerable<Box> GetPiece()
    //     {
    //         yield return this;

    //         foreach(Box linkedBox in linkedTo)
    //             foreach(Box linkedBox)
    //     }

    //     public IEnumerable<Coordinate> GetNeighboorToPiece(Coordinate direction, HashSet<Coordinate> belongsToPiece)
    //     {
    //         belongsToPiece.Add(position);

    //         foreach (Box box in linkedTo)
    //             foreach (Coordinate coord in box.GetNeighboorToPiece(direction, belongsToPiece))
    //                 if (!belongsToPiece.Contains(coord))
    //                     yield return coord;
            
    //         Coordinate neighbor = position + direction;
    //         if (!belongsToPiece.Contains(neighbor))
    //             yield return neighbor;
    //     }
    // }    

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
            foreach(BoxPart part in Parts)
            {
                neighbors.Add(part.localPosition + direction);
                neighbors.Remove(part.localPosition);
            }
            return neighbors.ToList();
        }
    }

    class BoxPart
    {
        public Coordinate localPosition;
        public Box Parent;

        public BoxPart(Coordinate pos, Box? parent = null)
        {
            localPosition = pos;
            Parent = parent;
        }

        public IEnumerable<Coordinate> GetNeighborCoordinates(Coordinate direction, Coordinate at)
        {
            foreach(Coordinate neighbor in Parent.GetNeighborCoordinates(direction))
                yield return neighbor - localPosition + at;
        }
    }
}
