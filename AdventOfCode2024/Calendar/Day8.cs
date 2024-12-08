using System;

namespace AdventOfCode2024.Calendar;

public class Day8 : IDayProblem
{
    public static int Day => 8;

    public static string SolvePart1(string input)
    {
        string[] map = input.Split('\n');
        HashSet<(int,int)> antinodePositions = [];
        Dictionary<char,List<(int,int)>> alreadySeenAntenna = [];

        foreach(Point point in AllPoint(map))
        {
            if (point.Antenna != '.')
                if(alreadySeenAntenna.TryGetValue(point.Antenna,out List<(int,int)> sameAntennaPositions))
                {
                    foreach((int,int) otherPosition in sameAntennaPositions)
                    {
                        (int,int) antinodeOffset = (point.Location.Item1 - otherPosition.Item1, point.Location.Item2  - otherPosition.Item2);
                        (int,int) antinode1 = (point.Location.Item1 + antinodeOffset.Item1, point.Location.Item2 + antinodeOffset.Item2);
                        (int,int) antinode2 = (otherPosition.Item1 - antinodeOffset.Item1, otherPosition.Item2 - antinodeOffset.Item2);
                        if (IsInMap(antinode1))
                            antinodePositions.Add(antinode1);
                        if (IsInMap(antinode2))
                            antinodePositions.Add(antinode2);
                    }
                    sameAntennaPositions.Add(point.Location);
                }
                else
                {
                    alreadySeenAntenna.Add(point.Antenna,[point.Location]);
                }
            
        }
        
        return antinodePositions.Count.ToString();
    }

    private static bool IsInMap((int,int) position)
    {
        return position.Item1 >= 0 && position.Item1 < 50 && position.Item2 >= 0 && position.Item2 < 50;
    }

    struct Point
    {
        public char Antenna;
        public (int,int) Location;

        public Point(char antennaValue, (int,int) loc)
        {
            Antenna = antennaValue;
            Location = loc;
        }
    }

    private static IEnumerable<Point> AllPoint(string[] map)
    {
        for(int row = 0; row < map.Length; ++row)
            for(int col = 0; col < map.Length; ++col)
                yield return new Point(map[row][col],(row,col));
    }

    private static IEnumerable<(int,int)> GenerateAntinodePosition((int,int) antenna1, (int,int) antenna2)
    {
        (int,int) antinodeOffset = (antenna1.Item1 - antenna2.Item1, antenna1.Item2 - antenna2.Item2);
        int multiplicator = 0;
        bool hasAddedElement = true;
        while (hasAddedElement)
        {
            hasAddedElement = false;

            (int,int) antinode = (antenna1.Item1 + antinodeOffset.Item1*multiplicator, antenna1.Item2 + antinodeOffset.Item2*multiplicator);
            if (IsInMap(antinode))
            {
                hasAddedElement = true;
                yield return antinode;
            }

            antinode = (antenna2.Item1 - antinodeOffset.Item1*multiplicator, antenna2.Item2 - antinodeOffset.Item2*multiplicator);
            if (IsInMap(antinode))
            {
                hasAddedElement = true;
                yield return antinode;
            }
            
            ++multiplicator;
        }
    }

    public static string SolvePart2(string input)
    {
        string[] map = input.Split('\n');
        HashSet<(int,int)> antinodePositions = [];
        Dictionary<char,List<(int,int)>> alreadySeenAntenna = [];

        foreach(Point point in AllPoint(map))
        {
            if (point.Antenna != '.')
            {
                if(alreadySeenAntenna.TryGetValue(point.Antenna,out List<(int,int)> sameAntennaPositions))
                {
                    foreach((int,int) otherPosition in sameAntennaPositions)
                        foreach((int,int) antinodePosition in GenerateAntinodePosition(point.Location, otherPosition))
                            antinodePositions.Add(antinodePosition);
                        
                    sameAntennaPositions.Add(point.Location);
                }
                else
                    alreadySeenAntenna.Add(point.Antenna,[point.Location]);
            }
        }
            
        
        return antinodePositions.Count.ToString();
    }
}
