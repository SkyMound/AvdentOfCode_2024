using System;
using System.Text.RegularExpressions;

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
        (int middleCol, int middleRow) = (bathroomWidth/2, bathroomHeight/2);
        (bool ignoreCol, bool ignoreRow) = (bathroomWidth%2!=0, bathroomHeight%2!=0);
        int[] quardantSafety;
        int maxSeconds = 10000;
        int secondsElapsed = 0;
        do
        {
            quardantSafety = [0,0,0,0];
            ++secondsElapsed;
            foreach(Robot robot in GetRobots(input))
            {
                int new_x =  (robot.x + (robot.v_x + bathroomWidth) * secondsElapsed) % bathroomWidth;
                int new_y =  (robot.y + (robot.v_y + bathroomHeight) * secondsElapsed) % bathroomHeight;

                if ((ignoreCol && new_x == middleCol) || (ignoreRow && new_y == middleRow))
                    continue;
                
                ++quardantSafety[Convert.ToInt32(new_x < middleCol) + Convert.ToInt32(new_y < middleRow) * 2];
            }

            if(quardantSafety[0] == quardantSafety[1] && quardantSafety[2] == quardantSafety[3])
            {
                Console.WriteLine(secondsElapsed);
                foreach(int i in quardantSafety)
                    Console.WriteLine(i);
            }

        }while(secondsElapsed < maxSeconds);



        return secondsElapsed.ToString();
    }
}
