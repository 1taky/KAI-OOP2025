namespace Lab4;

public static class StringOperations
{
    public delegate int StringOperation(string input);

    public static StringOperation CountLowerAnon = delegate (string str)
    {
        int count = 0;
        foreach (char c in str)
            if (!char.IsWhiteSpace(c)) count++;
        return count;
    };

    public static StringOperation CountLowerLambda = (str) =>
    {
        int count = 0;
        foreach (char c in str)
            if (!char.IsWhiteSpace(c)) count++;
        return count;
    };
}
