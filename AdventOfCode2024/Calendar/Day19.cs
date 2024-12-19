using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Calendar;

public class Day19 : IDayProblem
{
    public static int Day => 19;

    private static string[] GetTowels(string input) => input.Split("\r\n\r")[0].Split(", ",StringSplitOptions.TrimEntries);
    private static string[] GetTargets(string input) => input.Split("\r\n\r", StringSplitOptions.RemoveEmptyEntries)[1].Split('\n');

    public static string SolvePart1(string input)
    {
        string[] availablesTowels = GetTowels(input);
        string[] desiredDesigns = GetTargets(input);

        // foreach(string towel in desiredDesigns)
        //     Console.WriteLine(towel);
        // if (CanProduceDesign("bbrgwb", availablesTowels))
        //     Console.WriteLine("yes");

        int counter = 0;
        foreach(string targetDesign in desiredDesigns)
            if (CanProduceDesign(targetDesign, availablesTowels))
            {
                ++counter;
            }

        return (counter-1).ToString();
    }

    private static bool CanProduceDesign(string targetDesign, string[] availableTowels)
    {
        if (targetDesign.Trim().Length == 0)
            return true;

        foreach(string towel in availableTowels)
            if (targetDesign.StartsWith(towel))
                if (CanProduceDesign(targetDesign.Substring(towel.Length), availableTowels))
                    return true;

        return false;
    }

    private static List<string> GetPossibleArrangement(string targetDesign, string[] availableTowels)
    {
        if (targetDesign.Trim().Length == 0)
            return [""];

        List<string> arrangements = [];
        foreach(string towel in availableTowels)
            if (targetDesign.StartsWith(towel))
            {
                List<string> nextTowelsPossibilities = GetPossibleArrangement(targetDesign.Substring(towel.Length), availableTowels);
                foreach(string arrangement in nextTowelsPossibilities)
                {
                    arrangements.Add(towel+ ',' + arrangement);
                }
            }
                
        return arrangements;
    }

    class ColorNode
    {
        public int value;
        public bool last;
        public Dictionary<int,ColorNode> nextColors;

        public ColorNode()
        {
            value = -1;
            last = false;
            nextColors = [];
        }

        public ColorNode(int colorValue)
        {
            value = colorValue;
            last = false;
            nextColors = [];
        }
    }

    static Dictionary<char,int> CharConverter = new()
    {
        {'w', 0},
        {'u', 1},
        {'b', 2},
        {'r', 3},
        {'g', 4},
    };

    private static void AddToTree(ColorNode parent, int[] towelToInsert)
    {
        if (towelToInsert.Length != 0)
        {
            if(parent.nextColors.TryGetValue(towelToInsert[0], out ColorNode node))
                AddToTree(node, towelToInsert[1..]);
            else
            {
                ColorNode newNode = new(towelToInsert[0]);
                parent.nextColors.Add(towelToInsert[0], newNode);
                AddToTree(newNode, towelToInsert[1..]);
            }
        }
        else
            parent.last = true;
    }

    private static int Arrangements(ColorNode root, ColorNode current, int[] towel)
    {
        if (towel.Length == 0)
            return 1;
        
        int nbArrangements = 0;

        if (current.last)
            nbArrangements += Arrangements(root, root, towel);

        if (current.nextColors.TryGetValue(towel[0], out ColorNode nextNode))
            nbArrangements += Arrangements(root, nextNode, towel[1..]);

        return nbArrangements;
    }

    public static string SolvePart2(string input)
    {
        int[][] availablesTowels = input.Split("\r\n\r")[0].Split(", ",StringSplitOptions.TrimEntries).Select(towel => towel.Select(color => CharConverter[color]).ToArray()).ToArray();
        int[][] desiredDesigns = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)[1].Split("\r\n").Select(towel => towel.Select(color => CharConverter[color]).ToArray()).ToArray();

        ColorNode root = new();

        foreach(int[] towel in availablesTowels)
            AddToTree(root, towel);

        Arrangements(root,root,[2,3,0,3,3]);

        int designProcess = 0;
        int counter = 0;
        foreach(int[] design in desiredDesigns)
        {
            counter += Arrangements(root,root,design);
            designProcess++;
            Console.WriteLine(designProcess);
        }

        return counter.ToString();
    }
}
