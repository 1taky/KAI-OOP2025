using System;
using Entities;

namespace Lab1.Models;

public class Gardener: Human, IExtraSkill, IMakeJSON
{
    public string Plant { get; set; }


    public Gardener(string fName, string lName, Gender gdr, string plant) : base(fName, lName, gdr)
    {
        Plant = plant;
    }

    public string PlantAPlant()
    {
        return $"{FirstName} {LastName} has planted a plant";
    }

    public void SleepStanding()
    {
        Console.WriteLine($"{FirstName} {LastName} gardener is sleeping standing after planting");
    }

    public string[] MakeAsJSONFormat()
        {
            return
            [
                $"Gardener {FirstName}{LastName}",
                "{",
                $"\"firstname\": \"{FirstName}\",",
                $"\"lastname\": \"{LastName}\",",
                $"\"gender\": \"{Gender}\",",
                $"\"plant\": \"{Plant}\"",
            ];
        }
}
