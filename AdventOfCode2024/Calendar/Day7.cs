using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Calendar;

public class Day7 : IDayProblem
{
    public static int Day => 7;

    private static bool EquationSolver(long target, Stack<long> operands, bool useConcatenationOperator = false)
    {
        if(operands.Count == 1)
            return target == operands.Peek();

        if(operands.Peek() > target)
            return false;

        long operand1 = operands.Pop();
        long operand2 = operands.Pop();

        operands.Push(operand1*operand2);
        bool mulResult = EquationSolver(target,operands,useConcatenationOperator);
        operands.Pop();

        operands.Push(operand1+operand2);
        bool addResult = EquationSolver(target,operands,useConcatenationOperator);
        operands.Pop();

        bool concatResult = false;
        if(useConcatenationOperator)
        {
            operands.Push(long.Parse(operand1.ToString()+operand2.ToString()));
            concatResult = EquationSolver(target,operands,useConcatenationOperator);
            operands.Pop();
        }

        operands.Push(operand2);
        operands.Push(operand1);

        return mulResult | addResult | concatResult;
    }

    public static string SolvePart1(string input)
    {
        Regex numberRg = new(@"\d+");
        Stack<long>[] equations = input
            .Split('\n') 
            .Select(strEquation => new Stack<long>(numberRg.Matches(strEquation).Select(n => long.Parse(n.Value)).Reverse()))
            .ToArray();

        long totalCalibrationResult = 0;

        foreach(Stack<long> equation in equations)
        {
            long testValue = equation.Pop();
            if (EquationSolver(testValue, equation))
                totalCalibrationResult += testValue;
        }

        return totalCalibrationResult.ToString();
    }

    public static string SolvePart2(string input)
    {
        Regex numberRg = new(@"\d+");
        Stack<long>[] equations = input
            .Split('\n') 
            .Select(strEquation => new Stack<long>(numberRg.Matches(strEquation).Select(n => long.Parse(n.Value)).Reverse()))
            .ToArray();

        long totalCalibrationResult = 0;

        foreach(Stack<long> equation in equations)
        {
            long testValue = equation.Pop();
            if (EquationSolver(testValue, equation, true))
                totalCalibrationResult += testValue;
        }

        return totalCalibrationResult.ToString();
    }
    
}
