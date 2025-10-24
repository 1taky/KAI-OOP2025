using System;

namespace Lab4;

public sealed class Program
{
    public static void Main()
    {
        string test = "Hello from Car!";
        Console.WriteLine($"[Анонімний метод] Кількість малих літер: {StringOperations.CountLowerAnon(test)}");
        Console.WriteLine($"[Лямбда-вираз] Кількість малих літер: {StringOperations.CountLowerLambda(test)}");

        Console.WriteLine("\n--- Події автомобіля ---\n");

        Car car = new Car();

        car.Started += OnCarEvent;
        car.Moving += OnCarEvent;
        car.SpeedExceeded += OnCarEvent;
        car.Stopped += OnCarEvent;
        car.Refueled += OnCarEvent;

        car.Start();
        car.Move();
        car.Move();
        car.Stop();
        car.Refuel(15);
    }

    private static void OnCarEvent(object? sender, CarEvent e)
    {
        Console.WriteLine($"{e.Message} | Швидкість: {e.Speed} км/год | Паливо: {e.Fuel} л");
    }
}
