using System;
using System.Diagnostics.Metrics;
using System.Formats.Tar;
using System.Security.Cryptography.X509Certificates;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Calendar;

public class Day16 : IDayProblem
{
    public static int Day => 16;

    public static string SolvePart1(string input)
    {
        HashSet<Coordinate> walls = [];
        Coordinate startPosition = new();
        Coordinate targetPosition = new();
        string[] map = input.Split('\n');
        for(int row = 0; row < map.Length; ++row)
            for(int col = 0; col < map[row].Length; ++col)
            {
                if (map[row][col] == '#')
                    walls.Add(new(row,col));
                else if (map[row][col] == 'S')
                    startPosition = new(row,col);
                else if (map[row][col] == 'E')
                    targetPosition = new(row,col);
            }

        List<Path> paths = GetBestPaths(startPosition, targetPosition, 1, walls);

        return paths[0].score.ToString();
    }

    public static string SolvePart2(string input)
    {
        HashSet<Coordinate> walls = [];
        Coordinate startPosition = new();
        Coordinate targetPosition = new();
        string[] map = input.Split('\n');
        for(int row = 0; row < map.Length; ++row)
            for(int col = 0; col < map[row].Length; ++col)
            {
                if (map[row][col] == '#')
                    walls.Add(new(row,col));
                else if (map[row][col] == 'S')
                    startPosition = new(row,col);
                else if (map[row][col] == 'E')
                    targetPosition = new(row,col);
            }

        List<Path> bestPaths = GetBestPaths(startPosition, targetPosition, 1, walls);
        IEnumerable<Coordinate> tilesVisited = bestPaths.SelectMany(path => path.GetCoordinates());

        return tilesVisited.ToHashSet().Count.ToString();
    }

    class Path
    {
        public Coordinate coord;
        public Path parent;
        public int direction;
        public int score;

        public Path(Coordinate pos, int distance, int direction)
        {
            coord = pos;
            score = distance;
            parent = null;  
            this.direction = direction;
        }

        public IEnumerable<Coordinate> GetCoordinates()
        {
            yield return coord;
            if (parent != null)
                foreach(Coordinate parentCoord in parent.GetCoordinates())
                    yield return parentCoord;
        }
    }

    private static List<Path> GetBestPaths(Coordinate startPosition, Coordinate targetPosition, int startDirection, HashSet<Coordinate> walls)
    {
        PriorityQueue<Path,int> openList = new();
        Dictionary<(Coordinate,int),int> visited = [];
        openList.Enqueue(new(startPosition,0,startDirection),0);

        List<Path> bestPath = [];
        int currentBestScore = int.MaxValue;
        while(openList.Count != 0)
        {
            Path currentSquare = openList.Dequeue();
            for(int newDirection = currentSquare.direction -1; newDirection <= currentSquare.direction + 1; ++newDirection)
            {
                Coordinate newPosition = currentSquare.coord + Coordinate.Orthogonal[(newDirection+4)%4];

                Path newSquare = new(newPosition, currentSquare.score + (newDirection == currentSquare.direction ? 1 : 1001), (newDirection+4)%4);

                if (newSquare.score > currentBestScore)
                    continue;

                newSquare.parent = currentSquare;
                if (newSquare.coord == targetPosition)
                {
                    if (newSquare.score < currentBestScore)
                    {
                        currentBestScore = newSquare.score;
                        bestPath = [newSquare];
                    }
                    else if (newSquare.score == currentBestScore)
                        bestPath.Add(newSquare);
                    continue;
                }

                if (walls.Contains(newPosition))
                    continue;

                if (visited.TryGetValue((newPosition, newDirection), out int score))
                {
                    if (newSquare.score <= score)
                    {
                        visited[(newPosition, newDirection)] = newSquare.score;
                        openList.Enqueue(newSquare, newSquare.score);
                    }
                }
                else
                {
                    visited.Add((newPosition, newDirection),newSquare.score);
                    openList.Enqueue(newSquare, newSquare.score);
                }
                
            }
        }

        return bestPath;
    }
}
