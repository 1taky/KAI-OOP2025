using MemoryPack;
using System.Xml.Serialization;

namespace Lab3.DAL;

public enum Gender { Male, Female }

[MemoryPackable]
public partial class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Course { get; set; }
    public string StudentId { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string? Hostel { get; set; }

    // [XmlIgnore]
    public bool LivesInHostel => !string.IsNullOrWhiteSpace(Hostel);

    public Student() { }

    public override string ToString()
        => $"{LastName} {FirstName} | {Course} курс | {StudentId} | {Gender} | {Hostel ?? "Без гуртожитка"}";
}
