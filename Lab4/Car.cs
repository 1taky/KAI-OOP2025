namespace Lab4;

public sealed class Car
{
    public event EventHandler<CarEvent>? Started;
    public event EventHandler<CarEvent>? Moving;
    public event EventHandler<CarEvent>? Stopped;
    public event EventHandler<CarEvent>? Refueled;
    public event EventHandler<CarEvent>? SpeedExceeded;

    private const int MaxSpeed = 100;
    private int _speed;
    private double _fuel;

    public Car(int initialSpeed = 0, double initialFuel = 10)
    {
        _speed = initialSpeed;
        _fuel = initialFuel;
    }

    public void Start()
    {
        Started?.Invoke(this, new CarEvent("Автомобіль заведено.", _speed, _fuel));
    }

    public void Move(int delta = 50, double fuelUse = 2)
{
    _speed += delta;
    _fuel -= fuelUse;

    if (_speed >= MaxSpeed)
    {
        SpeedExceeded?.Invoke(this, new CarEvent("!!! Перевищення допустимої швидкості! !!!", _speed, _fuel));
    }
    else
    {
        Moving?.Invoke(this, new CarEvent("Автомобіль рухається.", _speed, _fuel));
    }
}

    public void Stop()
    {
        _speed = 0;
        Stopped?.Invoke(this, new CarEvent("Автомобіль зупинився.", _speed, _fuel));
    }

    public void Refuel(double amount)
    {
        _fuel += amount;
        Refueled?.Invoke(this, new CarEvent($"Заправлено на {amount} л.", _speed, _fuel));
    }
}
