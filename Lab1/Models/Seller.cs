using System;
using Entities;

namespace Lab1.Models;

public class Seller : Human, IExtraSkill
{
    public string Item { get; set; }


    public Seller(string fName, string lName, Gender gdr, string item) : base(fName, lName, gdr)
    {
        Item = item;
    }

    public string SellAnItem()
    {
        return $"{FirstName} {LastName} has sold an item";
    }

    public void SleepStanding()
    {
        Console.WriteLine($"{FirstName} {LastName} seller is sleeping standing after selling");
    }

    public string[] MakeAsJSONFormat()
        {
            return
            [
                $"Seller {FirstName}{LastName}",
                "{",
                $"\"firstname\": \"{FirstName}\",",
                $"\"lastname\": \"{LastName}\",",
                $"\"gender\": \"{Gender}\",",
                $"\"goods\": \"{Item}\"",
            ];
        }
}
