using Lab3.DAL;
using Lab3.BLL;
using System.Text.RegularExpressions;

namespace Lab3.PL;

public static class Menu
{
    public static void MainMenu()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Sleep standing demo");
        Seller seller = new Seller("Bob", "Bobovich", Gender.Male, "Phone");
        Gardener gardener = new Gardener("Emma", "Bobova", Gender.Female, "Sunflower");

        seller.SleepStanding();
        gardener.SleepStanding();
        Console.WriteLine();

        Console.WriteLine("=== Main menu ===");
        Console.WriteLine("1. JSON");
        Console.WriteLine("2. XML");
        Console.WriteLine("3. MemoryPack (Binary)");
        Console.WriteLine("4. Custom Text");
        Console.Write("Choose serialization: ");
        string? choice = Console.ReadLine();

        IDataProvider<Student> provider = choice switch
        {
            "2" => new XmlProvider<Student>(),
            "3" => new MemoryPackProvider<Student>(),
            "4" => new CustomProvider<Student>(),
            _ => new JSONProvider<Student>(),
        };

        string file = "students" + provider.FileExtension;
        var ctx = new EntityContext<Student>(provider, file);
        var service = new EntityService(ctx);

        while (true)
        {
            Console.WriteLine("\n--- Students ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Show all");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Calculate males in hostel and 3 course");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");
            string? option = Console.ReadLine();

            if (option == "0") break;

            try
            {
                switch (option)
                {
                    case "1":
                        AddStudent(service);
                        break;

                    case "2":
                        foreach (Student st in service.GetAllStudents())
                            Console.WriteLine(st);
                        break;

                    case "3":
                        Console.Write("Enter ID: ");
                        string id = Console.ReadLine() ?? "";
                        service.DeleteStudent(id);
                        Console.WriteLine("*** Deleted if exist ***");
                        break;

                    case "4":
                        Console.WriteLine($"Number of male 3rd course students in hostel: {service.CountMale3rdDorm()}");
                        break;

                    default:
                        Console.WriteLine("Wrong choice!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void AddStudent(EntityService service)
    {
        Console.Write("Last name: ");
        string ln = Console.ReadLine() ?? "";
        Console.Write("First name: ");
        string fn = Console.ReadLine() ?? "";
        Console.Write("Course: ");
        int course = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Student ID (6 digits): ");
        string studId = Console.ReadLine() ?? "";

        while (!Regex.IsMatch(studId, @"^\d{6}$"))
        {
            Console.Write("Invalid format. Try again (e.g. 123456): ");
            studId = Console.ReadLine() ?? "";
        }

        Console.Write("Gender (Male/Female): ");
        Gender gdr = Console.ReadLine()?.Trim().ToLower() == "male" ? Gender.Male : Gender.Female;
        Console.Write("Address: ");
        string address = Console.ReadLine() ?? "";
        Console.Write("Hostel (e.g. 3-412): ");
        string? hostel = Console.ReadLine();

        Student s = new Student
        {
            LastName = ln,
            FirstName = fn,
            Course = course,
            StudentId = $"KB{studId}",
            Gender = gdr,
            Address = address,
            Hostel = hostel
        };

        service.AddStudent(s);
        Console.WriteLine("*** Successfully added! ***");
    }
}
