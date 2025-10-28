using MemoryPack;

namespace Lab3.DAL;

[MemoryPackable]
public partial class Student : IExtraSkill
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Course { get; set; }
    public string StudentId { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string? Hostel { get; set; }

    public bool IsInHostel()
    {
        return !string.IsNullOrWhiteSpace(Hostel) && Hostel.Contains('-');
    }

    public Student() { }

    public override string ToString()
    {
        return $"{LastName} {FirstName} | {Course} курс | {StudentId} | {Gender} | {Hostel ?? "Без гуртожитку"}";
    }

    public void SleepStanding()
    {
        Console.WriteLine($"Student {FirstName} {LastName} is sleeping standing after studying");
    }
}
