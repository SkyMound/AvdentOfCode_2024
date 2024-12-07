using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Calendar;

public class Day7 : IDayProblem
{
    public static int Day => 7;

    private static bool EquationSolver(Int64 target, Stack<Int64> operands)
    {
        if(operands.Count == 1)
            return target == operands.Peek();
        
        Int64 operand1 = operands.Pop();
        Int64 operand2 = operands.Pop();

        operands.Push(operand1*operand2);
        bool mulResult = EquationSolver(target,operands);
        operands.Pop();

        operands.Push(operand1+operand2);
        bool addResult = EquationSolver(target,operands);
        operands.Pop();

        operands.Push(operand2);
        operands.Push(operand1);

        return mulResult | addResult;
    }

    public static string SolvePart1(string input)
    {
        Regex numberRg = new(@"\d+");
        Stack<Int64>[] equations = input
            .Split('\n') 
            .Select(strEquation => new Stack<Int64>(numberRg.Matches(strEquation).Select(n => Int64.Parse(n.Value)).Reverse()))
            .ToArray();
        
        Int64 totalCalibrationResult = 0;

        foreach(Stack<Int64> equation in equations)
        {
            Int64 testValue = equation.Pop();
            if (EquationSolver(testValue, equation))
                totalCalibrationResult += testValue;
        }

        return totalCalibrationResult.ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
    
}
