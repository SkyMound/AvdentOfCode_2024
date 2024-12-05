using System;

namespace AdventOfCode2024.Calendar;

public abstract class AdventDay
{
    public abstract int DayNumber {get;}
    private string InputPathURL {get => "https://adventofcode.com/2024/day/"+DayNumber.ToString()+"/input";} 
    private string DefaultFilePath {get => "C:\\Users\\tfresard\\Root\\projects\\AvdentOfCode_2024\\AdventOfCode2024\\Calendar\\Inputs\\"+DayNumber.ToString()+".txt";} 
    protected abstract string InternalRun(string input);

    public async Task<string> Run(string inputFilePath = "")
    {
        string path = inputFilePath.Length == 0 ? DefaultFilePath : inputFilePath;


            // using (HttpClient client = new HttpClient())
            // {
            //     try
            //     {
            //         // Send GET request
            //         Console.WriteLine(InputPathURL);
            //         HttpResponseMessage response = await client.GetAsync(InputPathURL);
            //         // Ensure success status code
            //         response.EnsureSuccessStatusCode();

            //         // Read the response as a string
            //         inputData = await response.Content.ReadAsStringAsync();
            //     }
            //     catch (HttpRequestException e)
            //     {
            //         Console.WriteLine($"Request error: {e.Message}");
            //         return string.Empty;
            //     }
            // }
        
        
        if(!File.Exists(path))
        {
            Console.WriteLine("No input found here : " + path);
            return string.Empty;
        }

        string inputData = await File.ReadAllTextAsync(path); 

        DateTime startTime = DateTime.Now;
        
        string result = InternalRun(inputData);
        Console.WriteLine(result);
        
        TimeSpan executionTime = DateTime.Now - startTime;

        Console.WriteLine("Took " + executionTime.ToString());

        return result;
    }

}
