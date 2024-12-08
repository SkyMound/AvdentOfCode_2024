using System;
using System.Runtime.InteropServices;

namespace AdventOfCode2024.Calendar;

public class Day4 : IDayProblem
{
    public static int Day => 4;

    private static int WordFinder(string wordToFind, string[] lettersBoard, (int,int) startingLetter)
    {
        int nbWord = 0;
        for (int rowOffset = -1; rowOffset<=1; ++rowOffset)
            for (int colOffset = -1; colOffset<=1; ++colOffset)
                if(!(rowOffset == 0 && colOffset == 0))
                    nbWord += _WordFinder(wordToFind, lettersBoard, startingLetter, (rowOffset,colOffset));
        return nbWord;
    }

    private static int _WordFinder(string wordToFind, string[] lettersBoard, (int,int) currentLetterPosition, (int,int) directionSearch)
    {
        ;
        if (currentLetterPosition.Item1 < 0 || currentLetterPosition.Item2 < 0 || currentLetterPosition.Item1 >= lettersBoard.Length || currentLetterPosition.Item2 >= lettersBoard.Length)
            return 0;

        if (wordToFind.Length==0)
            return 1;
        
        if (wordToFind[0] == lettersBoard[currentLetterPosition.Item1][currentLetterPosition.Item2])
            return _WordFinder(wordToFind.Remove(0,1), lettersBoard, (currentLetterPosition.Item1+directionSearch.Item1,currentLetterPosition.Item2+directionSearch.Item2), directionSearch);
        else 
            return 0;
    }

    public static string SolvePart1(string input)
    {
        string[] lettersBoard = input.Split('\n');

        string wordToFind = "XMAS";

        int numberWordFind = 0;

        for(int row = 0; row < lettersBoard.Length; ++row)
            for(int col = 0; col<lettersBoard[row].Length; ++col)
                if(lettersBoard[row][col] == wordToFind[0])
                    numberWordFind += WordFinder(wordToFind, lettersBoard, (row,col));        

        return numberWordFind.ToString();
    }

    public static string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}
