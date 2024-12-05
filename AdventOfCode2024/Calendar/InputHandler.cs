using System;

namespace AdventOfCode2024.Calendar;

public class InputHandler
{
    protected string path;

    public InputHandler(string filePath)
    {
        path = filePath;
        if(!File.Exists(path))
            throw new Exception("No input find");
    }
}
