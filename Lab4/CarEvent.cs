namespace Lab4;

public sealed class CarEvent : EventArgs
{
    public string Message { get; }
    public int Speed { get; }
    public double Fuel { get; }

    public CarEvent(string message, int speed = 0, double fuel = 0)
    {
        Message = message;
        Speed = speed;
        Fuel = fuel;
    }
}
