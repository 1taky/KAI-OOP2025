namespace Lab3.BLL;

public class InvalidStudentDataException : Exception
{
    public InvalidStudentDataException(string message) : base(message) { }
}
