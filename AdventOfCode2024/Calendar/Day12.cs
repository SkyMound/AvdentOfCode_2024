using System;
using System.Formats.Asn1;
using System.Xml.XPath;

namespace AdventOfCode2024.Calendar;

public class Day12 : IDayProblem
{
    public static int Day => 12;

    private static (int,int)[] OrthogonalOffset = [(0,1),(1,0),(-1,0),(0,-1)];

    private static bool IsInMap((int,int) position, char[][] map)
    {
        return position.Item1 >= 0 && position.Item1 < map.Length && position.Item2 >= 0 && position.Item2 < map[0].Length;
    }

    private static (int,int) ComputeAreaAndPerimeter(char[][] mapRegion, char plantType, (int,int) currentGarden, HashSet<(int,int)> gardenVisited)
    {
        if (gardenVisited.Contains(currentGarden))
            return (0,0);

        if (!IsInMap(currentGarden, mapRegion))
            return (0,1);

        if (mapRegion[currentGarden.Item1][currentGarden.Item2] != plantType)
            return (0,1);

        gardenVisited.Add(currentGarden);
        int area = 0, perimeter = 0;
        foreach( (int,int) offset in OrthogonalOffset)
        {
            (int,int) newGarden = (currentGarden.Item1 + offset.Item1, currentGarden.Item2 + offset.Item2);
            (int,int) result = ComputeAreaAndPerimeter(mapRegion, plantType, newGarden, gardenVisited);
            area += result.Item1;
            perimeter += result.Item2;
        }
        return (area + 1,perimeter);
    }

    private static (int,int) ComputeAreaAndSide(char[][] mapRegion, char plantType, (int,int) currentGarden, HashSet<(int,int)> gardenVisited)
    {
        if (gardenVisited.Contains(currentGarden))
            return (0,0);

        gardenVisited.Add(currentGarden);
        int area = 0, side = 0;
        for(int offsetIndex = 0; offsetIndex < OrthogonalOffset.Length; ++offsetIndex)
        {
            (int,int) offset = OrthogonalOffset[offsetIndex];
            (int,int) nextOffset = OrthogonalOffset[(offsetIndex+1)%4];
            (int,int) diagonal = (offset.Item1 + nextOffset.Item1, offset.Item2 + nextOffset.Item2);

            (int,int) newGarden = (currentGarden.Item1 + offset.Item1, currentGarden.Item2 + offset.Item2);
            (int,int) cornerTestGarden = (currentGarden.Item1 + nextOffset.Item1, currentGarden.Item2 + nextOffset.Item2);
            (int,int) diagonalGarden = (currentGarden.Item1 + diagonal.Item1, currentGarden.Item2 + diagonal.Item2);


            if(!IsInMap(newGarden, mapRegion) || plantType != mapRegion[newGarden.Item1][newGarden.Item2])
                if (!IsInMap(cornerTestGarden, mapRegion) || plantType != mapRegion[cornerTestGarden.Item1][cornerTestGarden.Item2]) // Convex corner check
                    ++side;
            else
            {
                if (IsInMap(cornerTestGarden, mapRegion) && plantType == mapRegion[cornerTestGarden.Item1][cornerTestGarden.Item2]
                && IsInMap(diagonalGarden, mapRegion) && plantType != mapRegion[diagonalGarden.Item1][diagonalGarden.Item2]) // Concave corner check
                    ++side;
                (int,int) result = ComputeAreaAndSide(mapRegion, plantType, newGarden, gardenVisited);
                area += result.Item1;
                side += result.Item2;
            }
        }

        return (area +1 ,side);
    }

    public static string SolvePart1(string input)
    {
        char[][] regionMap = input.Replace("\r", string.Empty).Split('\n').Select(str => str.ToCharArray()).ToArray();

        HashSet<(int,int)> gardenPartOfRegion = [];

        int price = 0;

        for (int row = 0; row < regionMap.Length; ++row)
            for (int col = 0; col < regionMap[0].Length; ++col)
                if (!gardenPartOfRegion.Contains((row,col)))
                {
                    HashSet<(int,int)> thisRegion = [];
                    (int,int) result = ComputeAreaAndPerimeter(regionMap, regionMap[row][col], (row,col), thisRegion);
                    Console.WriteLine($"Plant : {regionMap[row][col]} - Area : {result.Item1} - Side : {result.Item2}");
                    price += result.Item1 * result.Item2;
                    gardenPartOfRegion.UnionWith(thisRegion);
                }
    
        return price.ToString();
    }

    public static string SolvePart2(string input)
    {
        char[][] regionMap = input.Replace("\r", string.Empty).Split('\n').Select(str => str.ToCharArray()).ToArray();

        HashSet<(int,int)> gardenPartOfRegion = [];

        int price = 0;

        for (int row = 0; row < regionMap.Length; ++row)
            for (int col = 0; col < regionMap[0].Length; ++col)
                if (!gardenPartOfRegion.Contains((row,col)))
                {
                    HashSet<(int,int)> thisRegion = [];
                    (int,int) result = ComputeAreaAndSide(regionMap, regionMap[row][col], (row,col), thisRegion);
                    Console.WriteLine($"Plant : {regionMap[row][col]} - Area : {result.Item1} - Side : {result.Item2}");
                    price += result.Item1 * result.Item2;
                    gardenPartOfRegion.UnionWith(thisRegion);
                }
    
        return price.ToString();
    }
}
