namespace Lab3.DAL;

public class Gardener : Human, IExtraSkill
{
    public string Plant { get; set; }


    public Gardener(string fName, string lName, Gender gdr, string plant) : base(fName, lName, gdr)
    {
        Plant = plant;
    }

    public string PlantAPlant()
    {
        return $"{FirstName} {LastName} has planted a {Plant}";
    }

    public void SleepStanding()
    {
        Console.WriteLine($"{FirstName} {LastName} gardener is sleeping standing after planting {Plant}");
    }
}
