using System;

namespace AdventOfCode2024.Calendar;

public class Day23 : IDayProblem
{
    public static int Day => 23;

    public static string SolvePart1(string input)
    {
        string[] connections = input.Split("\r\n");
        HashSet<Computer> allComputers = [];

        // Building the graph
        foreach (string connection in connections)
        {
            string[] computers = connection.Split('-');
            Computer computer1 = new(computers[0]);
            Computer computer2 = new(computers[1]);
            if (allComputers.TryGetValue(computer1, out var comp))
                comp.connectedTo.Add(computer2);
            else
            {
                computer1.connectedTo.Add(computer2);
                allComputers.Add(computer1);
            }

            if (allComputers.TryGetValue(computer2, out var comp2))
                comp2.connectedTo.Add(computer1);
            else
            {
                computer2.connectedTo.Add(computer1);
                allComputers.Add(computer2);
            }
        }

        int nbLAN = 0;
        HashSet<Computer> processed = [];
        
        foreach(Computer computer in allComputers)
        {
            if (computer.name.StartsWith('t'))
            {
                foreach(Computer linkedTo in computer.connectedTo)
                {
                    if (linkedTo.connectedTo.Intersect(computer.connectedTo))
                }
            }
        }

        return string.Empty;
    }

    class Computer
    {
        public string name;
        public HashSet<Computer> connectedTo;
        public override int GetHashCode() => name.GetHashCode();

        public Computer(string name)
        {
            this.name = name;
            connectedTo = [];
        }

        public Computer(string name, Computer computerLinked)
        {
            this.name = name;
            connectedTo = [computerLinked];
        }
        public override bool Equals(object obj)
        {
            if (obj is Computer other)
            {
                return name == other.name;
            }
            return false;
        }
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}
