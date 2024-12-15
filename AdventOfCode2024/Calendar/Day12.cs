using System;
using System.Formats.Asn1;
using System.Linq;
using System.Xml.XPath;

namespace AdventOfCode2024.Calendar;

public class Day12 : IDayProblem
{
    public static int Day => 12;

    public static string SolvePart1(string input)
    {
        char[][] plotsMap = GetPlotsMap(input);
    
        return GetAllRegionMetrics(plotsMap).Sum(region => region.area * region.perimeter).ToString();
    }

    public static string SolvePart2(string input)
    {
        char[][] plotsMap = GetPlotsMap(input);
    
        return GetAllRegionMetrics(plotsMap).Sum(region => region.area * region.sides).ToString();
    }

    private static readonly (int,int)[] OrthogonalOffset = [(0,1),(1,0),(0,-1),(-1,0)];

    private static char[][] GetPlotsMap(string input) => input.Replace("\r", string.Empty).Split('\n').Select(str => str.ToCharArray()).ToArray();

    private static bool IsInMap((int,int) position, char[][] map)
    {
        return position.Item1 >= 0 && position.Item1 < map.Length && position.Item2 >= 0 && position.Item2 < map[0].Length;
    }

    private static IEnumerable<(int area, int perimeter, int sides)> GetAllRegionMetrics(char[][] plotsMap)
    {
        HashSet<(int,int)> plotsVisited = [];

        for (int row = 0; row < plotsMap.Length; ++row)
            for (int col = 0; col < plotsMap[0].Length; ++col)
                if (!plotsVisited.Contains((row,col)))
                    yield return ComputeRegionMetrics(plotsMap, (row,col), plotsMap[row][col], plotsVisited);
    }

    private static (int area, int perimeter, int sides) ComputeRegionMetrics(char[][] plotsMap, (int row, int col) currentPlot, char plantType, HashSet<(int,int)> plotsVisited)
    {
        if (plotsVisited.Contains(currentPlot))
            return (0,0,0);

        plotsVisited.Add(currentPlot);
        int area = 1, perimeter = 0, sides = 0;
        for(int offsetIndex = 0; offsetIndex < OrthogonalOffset.Length; ++offsetIndex)
        {
            (int rowOffset, int colOffset) = OrthogonalOffset[offsetIndex];
            (int rowPerpOffset, int colPerpOffset) = OrthogonalOffset[(offsetIndex+1)%4];
            (int rowDiagonalOffset, int colDiagonalOffset) = (rowOffset + rowPerpOffset, colOffset + colPerpOffset);

            (int,int) nextPlot = (currentPlot.row + rowOffset, currentPlot.col + colOffset);
            (int,int) perpPlot = (currentPlot.row + rowPerpOffset, currentPlot.col + colPerpOffset);
            (int,int) diagonalPlot = (currentPlot.row + rowDiagonalOffset, currentPlot.col + colDiagonalOffset);

            if(!IsInMap(nextPlot, plotsMap) || plantType != plotsMap[nextPlot.Item1][nextPlot.Item2])
            {
                ++perimeter;
                if (!IsInMap(perpPlot, plotsMap) || plantType != plotsMap[perpPlot.Item1][perpPlot.Item2]) // Convex corner check
                    ++sides;
            }
            else
            {
                if (IsInMap(perpPlot, plotsMap) && plantType == plotsMap[perpPlot.Item1][perpPlot.Item2]
                && IsInMap(diagonalPlot, plotsMap) && plantType != plotsMap[diagonalPlot.Item1][diagonalPlot.Item2]) // Concave corner check
                    ++sides;
                (int subArea, int subPerimeter, int subSides) =  ComputeRegionMetrics(plotsMap, nextPlot, plantType, plotsVisited);
                area += subArea;
                perimeter += subPerimeter;
                sides += subSides;
            }
        }

        return (area, perimeter, sides);
    }
}
