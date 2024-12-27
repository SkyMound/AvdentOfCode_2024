using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Calendar;

public class Day14 : IDayProblem
{
    public static int Day => 14;

    struct Robot {
        public int x = 0;
        public int y = 0;
        public int v_x = 0;
        public int v_y = 0;

        public Robot()
        {
        }
    }

    struct RobotCoord {
        public Coordinate position;
        public Coordinate velocity;

        public RobotCoord()
        {
            position = new();
            velocity = new();
        }
    }

    private static IEnumerable<Robot> GetRobots(string input) 
    {
        Regex regex = new Regex(@"-?\d+");

        foreach(string strMachine in input.Split("\n"))
        {
            MatchCollection matches = regex.Matches(strMachine);
            Robot r = new();
            r.x = int.Parse(matches[0].Value);
            r.y = int.Parse(matches[1].Value);
            r.v_x = int.Parse(matches[2].Value);
            r.v_y = int.Parse(matches[3].Value);
            yield return r;
        }
    }

    //     private static IEnumerable<Robot> GetRobots(string input) 
    // {
    //     Regex regex = new Regex(@"-?\d+");

    //     foreach(string strMachine in input.Split("\n"))
    //     {
    //         MatchCollection matches = regex.Matches(strMachine);
    //         Robot r = new();
    //         r.x = int.Parse(matches[0].Value);
    //         r.y = int.Parse(matches[1].Value);
    //         r.v_x = int.Parse(matches[2].Value);
    //         r.v_y = int.Parse(matches[3].Value);
    //         yield return r;
    //     }
    // }

    public static string SolvePart1(string input)
    {
        (int bathroomWidth, int bathroomHeight) = (101,103);
        (int middleCol, int middleRow) = (bathroomWidth/2, bathroomHeight/2);
        (bool ignoreCol, bool ignoreRow) = (bathroomWidth%2!=0, bathroomHeight%2!=0);
        int[] quardantSafety = [0,0,0,0];
        int seconds = 100;

        foreach(Robot robot in GetRobots(input))
        {
            int new_x =  (robot.x + (robot.v_x + bathroomWidth) * seconds) % bathroomWidth;
            int new_y =  (robot.y + (robot.v_y + bathroomHeight) * seconds) % bathroomHeight;

            if ((ignoreCol && new_x == middleCol) || (ignoreRow && new_y == middleRow))
                continue;
            
            ++quardantSafety[Convert.ToInt32(new_x < middleCol) + Convert.ToInt32(new_y < middleRow) * 2];
        }

        return quardantSafety.Aggregate(1, (acc, x) => acc * x).ToString();
    }

    public static string SolvePart2(string input)
    {
        (int bathroomWidth, int bathroomHeight) = (101,103);
        const int MAX_SECONDS = 10000;

        int secondsMin = 0;
        int distanceMin = int.MaxValue;

        IEnumerable<Robot> robots = GetRobots(input);
        for(int seconds = 0; seconds < MAX_SECONDS; ++seconds)
        {
            HashSet<Coordinate> adjacentSpaces = [];
            foreach(Robot robot in robots)
            {
                int new_x =  (robot.x + (robot.v_x + bathroomWidth) * seconds) % bathroomWidth;
                int new_y =  (robot.y + (robot.v_y + bathroomHeight) * seconds) % bathroomHeight;

                foreach(Coordinate coord in new Coordinate(new_x, new_y).GetOrthogonals())
                    adjacentSpaces.Add(coord);

                if (adjacentSpaces.Count > distanceMin)
                    break;
            }

            if (adjacentSpaces.Count < distanceMin)
            {
                Console.WriteLine(seconds);
                secondsMin = seconds;
                distanceMin = adjacentSpaces.Count;
            }
        }

        Display(robots, secondsMin);
        return secondsMin.ToString();
    }


    private static void Display(IEnumerable<Robot> robots, int seconds)
    {
        (int bathroomWidth, int bathroomHeight) = (101,103);
        List<Robot> rob = robots.ToList();
        HashSet<Coordinate> robotPosition = [];
        rob.ForEach(rob => 
        {
            robotPosition.Add(new(rob.y + (rob.v_y + bathroomHeight) * seconds % bathroomHeight, rob.x + (rob.v_x + bathroomWidth) * seconds % bathroomWidth));
            // rob.x += (rob.v_x + bathroomWidth) * seconds % bathroomWidth;
            // rob.y += (rob.v_y + bathroomWidth) * seconds % bathroomWidth;
        });

        for(int i = 0; i < bathroomWidth; ++i)
        {
            for(int j = 0; j < bathroomHeight; ++j)
            {
                if (robotPosition.Contains(new(i,j)))
                    Console.Write('X');
                else
                    Console.Write(' ');
            }
            Console.WriteLine('#');
        }
    }
    // public static string SolvePart2(string input)
    // {
    //     (int bathroomWidth, int bathroomHeight) = (101,103);
    //     (int middleCol, int middleRow) = (bathroomWidth/2, bathroomHeight/2);
    //     (bool ignoreCol, bool ignoreRow) = (bathroomWidth%2!=0, bathroomHeight%2!=0);
    //     int[] quardantSafety;
    //     int maxSeconds = 10000;
    //     int secondsElapsed = 0;
    //     do
    //     {
    //         quardantSafety = [0,0,0,0];
    //         ++secondsElapsed;
    //         foreach(Robot robot in GetRobots(input))
    //         {
    //             int new_x =  (robot.x + (robot.v_x + bathroomWidth) * secondsElapsed) % bathroomWidth;
    //             int new_y =  (robot.y + (robot.v_y + bathroomHeight) * secondsElapsed) % bathroomHeight;

    //             if ((ignoreCol && new_x == middleCol) || (ignoreRow && new_y == middleRow))
    //                 continue;

    //             ++quardantSafety[Convert.ToInt32(new_x < middleCol) + Convert.ToInt32(new_y < middleRow) * 2];
    //         }

    //         if(quardantSafety[0] == quardantSafety[1] && quardantSafety[2] == quardantSafety[3])
    //         {
    //             Console.WriteLine(secondsElapsed);
    //             foreach(int i in quardantSafety)
    //                 Console.WriteLine(i);
    //         }

    //     }while(secondsElapsed < maxSeconds);



    //     return secondsElapsed.ToString();
    // }
}