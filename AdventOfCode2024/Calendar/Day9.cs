using System;
using System.Text;

namespace AdventOfCode2024.Calendar;

public class Day9 : IDayProblem
{
    public static int Day => 9;

    public static string SolvePart1(string input)
    {
        List<int> sizeMap = input
            .Select(c => int.Parse(c.ToString()))
            .ToList();
        
        List<int> compactedFile = [];

        int leftIndex = 0;      // index of the leftmost block not processed
        int rightIndex = sizeMap.Count % 2 == 0 ? sizeMap.Count - 2 : sizeMap.Count - 1; // index of the rightmost block not processed

        int leftMostFileID = 0; // ID of the leftmost file not processed
        int rightMostFileID = (sizeMap.Count-1)/2;  // ID of the rightmost file not processed

        int nbBlockToMoveRemaining = sizeMap[rightIndex]; // Number of remaining blocks of the rightmost file to move

        while(leftIndex < rightIndex)
        {
            if (leftIndex % 2 == 0) // If file
            {
                for(int _ = 0; _ < sizeMap[leftIndex]; ++_)
                    compactedFile.Add(leftMostFileID);
                ++leftMostFileID;
            }
            else // If free space
            {
                for(int _ = 0; _ < sizeMap[leftIndex]; ++_)
                {
                    if (nbBlockToMoveRemaining == 0) // If we moved every block of the rightmost file, we go to the the next one
                    {
                        rightIndex -= 2; // We ignore free space
                        --rightMostFileID;
                        nbBlockToMoveRemaining = sizeMap[rightIndex];
                    }
                    compactedFile.Add(rightMostFileID);
                    --nbBlockToMoveRemaining;
                }
            }
            ++leftIndex;
        }

        // We add the remaining block
        for(int _ = 0; _ < nbBlockToMoveRemaining; ++_)
            compactedFile.Add(rightMostFileID);

        return ComputeChecksum(compactedFile).ToString();
    }

    private static long ComputeChecksum(List<int> compactedDiskMap)
    {
        long checksum = 0;
        for (int blockPosition = 0; blockPosition < compactedDiskMap.Count; ++blockPosition)
            checksum += compactedDiskMap[blockPosition] * blockPosition;
        return checksum;
    }

    public static string SolvePart2(string input)
    {
        List<int> sizeMap = input
            .Select(c => int.Parse(c.ToString()))
            .ToList();

        List<int> compactedFile = [];
        HashSet<int> movedFilesID = [];

        int rightMostFileIndex = sizeMap.Count % 2 == 0 ? sizeMap.Count - 2 : sizeMap.Count - 1;
        int leftFileID = 0;

        for(int leftFileIndex = 0; leftFileIndex < sizeMap.Count; ++leftFileIndex)
        {
            if (leftFileIndex % 2 == 0) // if file
            {
                if (!movedFilesID.Contains(leftFileID))
                    for(int i = 0; i < sizeMap[leftFileIndex]; ++i)
                        compactedFile.Add(leftFileID);
                else
                    for(int i = 0; i < sizeMap[leftFileIndex]; ++i)
                        compactedFile.Add(0);
                ++leftFileID;
            }
            else // if free space
            {
                int spaceAvailable = sizeMap[leftFileIndex];
                bool hasFoundFileFitting = true;
                while (spaceAvailable > 0 && hasFoundFileFitting) // repeat if there is still space after placing a file
                {
                    hasFoundFileFitting = false;
                    // We iter on file starting from the end to see if one could fit in the free space 
                    int fileID = (sizeMap.Count-1)/2; 
                    for(int fileIndex = rightMostFileIndex; fileIndex > leftFileIndex; fileIndex-=2)
                    {
                        int fileSize = sizeMap[fileIndex];
                        if (fileSize <= spaceAvailable && !movedFilesID.Contains(fileID))
                        {
                            for(int i = 0; i < fileSize; ++i)
                                compactedFile.Add(fileID);
                            movedFilesID.Add(fileID);
                            spaceAvailable -= fileSize;
                            hasFoundFileFitting = true;
                            break;
                        }
                        --fileID;
                    }
                }

                // We add 0 for the space that couldn't be filled with file
                for(int i = 0; i < spaceAvailable; ++i)
                    compactedFile.Add(0);
            }
        }

        return ComputeChecksum(compactedFile).ToString();
    }
}
