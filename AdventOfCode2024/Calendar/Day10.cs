using System;

namespace AdventOfCode2024.Calendar;

public class Day10 : IDayProblem
{
    public static int Day => 10;

    private static (int,int)[] OrthogonalOffset = [(0,1),(1,0),(-1,0),(0,-1)];

    private static bool IsInMap((int,int) position, int[][] map)
    {
        return position.Item1 >= 0 && position.Item1 < map.Length && position.Item2 >= 0 && position.Item2 < map[0].Length;
    }
    private static int GetHikingTrailScore(int[][] map, (int,int) position, HashSet<(int,int)> alreadyVisited)
    {
        if (alreadyVisited.Contains(position))
            return 0;

        alreadyVisited.Add(position);

        if (map[position.Item1][position.Item2] == 9)
            return 1;

        int score = 0;
        int currentHeight = map[position.Item1][position.Item2];
        foreach( (int,int) offset in OrthogonalOffset)
        {
            (int,int) newPosition = (position.Item1 + offset.Item1, position.Item2 + offset.Item2);
            if (IsInMap(newPosition,map))
                if (map[newPosition.Item1][newPosition.Item2] == currentHeight + 1)
                    score += GetHikingTrailScore(map, newPosition, alreadyVisited);
        }
        return score;
    }

    private static int GetHikingTrailRating(int[][] map, (int,int) position)
    {
        if (map[position.Item1][position.Item2] == 9)
            return 1;

        int score = 0;
        int currentHeight = map[position.Item1][position.Item2];
        foreach( (int,int) offset in OrthogonalOffset)
        {
            (int,int) newPosition = (position.Item1 + offset.Item1, position.Item2 + offset.Item2);
            if (IsInMap(newPosition,map))
                if (map[newPosition.Item1][newPosition.Item2] == currentHeight + 1)
                    score += GetHikingTrailRating(map, newPosition);
        }
        return score;
    }

    public static string SolvePart1(string input)
    {
        int[][] topologicMap = input.Replace("\r", string.Empty).Split('\n').Select(str => str.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        int trailheadScoreSum = 0;

        for(int row = 0; row < topologicMap.Length; ++row)
            for(int col = 0; col < topologicMap[0].Length; ++col)
                if (topologicMap[row][col] == 0)
                    trailheadScoreSum += GetHikingTrailScore(topologicMap, (row,col),[]);
        
        return trailheadScoreSum.ToString();
    }

    public static string SolvePart2(string input)
    {
        int[][] topologicMap = input.Replace("\r", string.Empty).Split('\n').Select(str => str.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        int trailheadRatingSum = 0;

        for(int row = 0; row < topologicMap.Length; ++row)
            for(int col = 0; col < topologicMap[0].Length; ++col)
                if (topologicMap[row][col] == 0)
                    trailheadRatingSum += GetHikingTrailRating(topologicMap, (row,col));
        
        return trailheadRatingSum.ToString();
    }
}
