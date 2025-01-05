using System;
using System.Text;

namespace AdventOfCode2024.Calendar;

public class Day17 : IDayProblem
{
    public static int Day => 17;

    public static string SolvePart1(string input)
    {
        string[] lines = input.Split('\n');
        long[] registers = [int.Parse(lines[0].Substring(11)),int.Parse(lines[1].Substring(11)),int.Parse(lines[2].Substring(11))];
        int[] program = input.Split("\n\r")[1][10..].Split(',').Select(int.Parse).ToArray();

        return RunProgram(registers, program);
    }

    private static string RunProgram(long[] registers, int[] program)
    {
        StringBuilder output = new();
        bool increasePointer = true;
        int instructionsPointer = 0;

        while(instructionsPointer < program.Length)
        {
            int opcode = program[instructionsPointer];
            int operand = program[instructionsPointer+1];
            // Console.WriteLine($"Opcode : {opcode} - Operand {operand}");
            switch(opcode) 
            {
                case 0: // adv
                    Console.WriteLine(registers[0]);
                    registers[0] /= (int)Math.Pow(2,GetCombo(operand,registers));
                    Console.WriteLine(registers[0]);
                    break;
                case 1: // bxl
                    registers[1] ^= operand;
                    break;
                case 2 : // bst
                    registers[1] = GetCombo(operand,registers) % 8;
                    break;
                case 3 : // jnz
                    if (registers[0] != 0)
                    {
                        increasePointer = false;
                        instructionsPointer = operand;
                    }
                    break;
                case 4 : // bxc 
                    registers[1] ^= registers[2];
                    break;
                case 5 : // out
                    output.Append((GetCombo(operand,registers)%8).ToString() + ',');
                    break;
                case 6 : // bdv
                    registers[1] = registers[0] / (int)Math.Pow(2,GetCombo(operand,registers));
                    break;
                case 7 : // cdv
                    registers[2] = registers[0] / (int)Math.Pow(2,GetCombo(operand,registers));
                    break;
            }

            if (increasePointer)
                instructionsPointer+=2;
            else
                increasePointer = true;
        }
        return output.ToString();
    }

    
    private static long GetCombo(int operand, long[] registers)
    {
        if (operand > 3 && operand < 7)
            return registers[operand - 4];
        return operand;
    }

    public static string SolvePart2(string input)
    {
        string[] lines = input.Split('\n');
        long[] registers = [long.Parse(lines[0].Substring(11)),long.Parse(lines[1].Substring(11)),long.Parse(lines[2].Substring(11))];
        int[] program = input.Split("\n\r")[1][10..].Split(',').Select(int.Parse).ToArray();
        int outputLeft = program.Length;

        // long aRegister = 1 << 48;
        long aRegister = 1 << 3 * 5;
        Console.WriteLine(aRegister);
        for(long a = 0; a < 10000000000000; ++a)
        {
            registers[0] = aRegister + a;
            StringBuilder output = new();
            bool increasePointer = true;
            int instructionsPointer = 0;
            int currentProgramTarget = 0;
            bool isValid = true;

            while(instructionsPointer < program.Length && isValid)
            {
                int opcode = program[instructionsPointer];
                int operand = program[instructionsPointer+1];
                // Console.WriteLine($"Opcode : {opcode} - Operand {operand}");
                switch(opcode) 
                {
                    case 0: // adv
                        registers[0] /= (long)Math.Pow(2,GetCombo(operand,registers));
                        break;
                    case 1: // bxl
                        registers[1] ^= operand;
                        break;
                    case 2 : // bst
                        registers[1] = GetCombo(operand,registers) % 8;
                        break;
                    case 3 : // jnz
                        if (registers[0] != 0)
                        {
                            increasePointer = false;
                            instructionsPointer = operand;
                        }
                        break;
                    case 4 : // bxc 
                        registers[1] ^= registers[2];
                        break;
                    case 5 : // out
                        long outputResult = GetCombo(operand,registers)%8;
                        if (currentProgramTarget < program.Length && outputResult == program[currentProgramTarget])
                        {
                            output.Append(outputResult.ToString() + ',');
                            ++currentProgramTarget;
                        }
                        else 
                            isValid = false;
                        break;
                    case 6 : // bdv
                        registers[1] = registers[0] / (int)Math.Pow(2,GetCombo(operand,registers));
                        break;
                    case 7 : // cdv
                        registers[2] = registers[0] / (int)Math.Pow(2,GetCombo(operand,registers));
                        break;
                }

                if (increasePointer)
                    instructionsPointer+=2;
                else
                    increasePointer = true;
            }

            if (isValid && currentProgramTarget == program.Length)
            {
                Console.WriteLine(output.ToString());
                return (aRegister + a).ToString();
            }

        }
        return "0";
    }
}
