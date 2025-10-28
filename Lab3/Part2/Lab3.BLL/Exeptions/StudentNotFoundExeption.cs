namespace Lab3.BLL;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string message) : base(message) { }
}
