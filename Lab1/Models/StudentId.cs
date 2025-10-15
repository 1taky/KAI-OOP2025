using System;
using System.Text.RegularExpressions;

namespace Lab1.Models;

public class StudentId
{
public string Letters { get; } = "KB";
public int Numbers { get; }
public string FullID => $"{Letters}{Numbers:D6}";

    public StudentId(string letters, int numbers)
    {
        Letters = letters ?? "";
        Numbers = numbers;
    }

    public StudentId(int nums)
    {
        Numbers = nums;
    }
}