using Lab3.DAL;
using Lab3.BLL;
using System.Text.RegularExpressions;

namespace Lab3.PL;

public static class Menu
{
    public static void MainMenu()
    {
        Console.WriteLine("=== Main menu ===");
        Console.WriteLine("1. JSON");
        Console.WriteLine("2. XML");
        Console.WriteLine("3. MemoryPack (Binary)");
        Console.Write("Choose serialization: ");
        string? choice = Console.ReadLine();

        IDataProvider<Student> provider;

        switch (choice)
        {
            case "2":
                provider = new XmlProvider<Student>();
                break;
            case "3":
                provider = new MemoryPackProvider<Student>();
                break;
            default:
                provider = new JSONProvider<Student>();
                break;
        }

        string file = "students" + provider.FileExtension;
        var ctx = new EntityContext<Student>(provider, file);
        var service = new EntityService(ctx);

        while (true)
        {
            Console.WriteLine("\n--- Students ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Show all");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Calculate males in hostlen and 3 course");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");
            string? option = Console.ReadLine();

            if (option == "0") break;

            switch (option)
            {
                case "1":
                    Console.Write("Last name: ");
                    string ln = Console.ReadLine() ?? "";
                    Console.Write("First name: ");
                    string fn = Console.ReadLine() ?? "";
                    Console.Write("Course: ");
                    int course = int.Parse(Console.ReadLine());
                    Console.Write("StudID number: ");
                    string studId = Console.ReadLine();
                    string studIdPatern = @"^\d{6}$";
                    if(!Regex.IsMatch(studId, studIdPatern))
                    {
                        while (!Regex.IsMatch(studId, studIdPatern))
                        {
                            Console.Write("Incorrect format. (eg: 123456). please try again: ");
                            studId = Console.ReadLine();
                        }
                    }
                    Console.Write("Gender (Male/Female): ");
                    Gender gdr = Console.ReadLine()?.ToLower() == "male" ? Gender.Male : Gender.Female;
                    Console.Write("Adress: ");
                    string address = Console.ReadLine() ?? "";
                    Console.Write("Hostel (ex. 3-412): ");
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
                    break;

                case "2":
                    foreach (Student st in service.GetAllStudents())
                        Console.WriteLine(st);
                    break;

                case "3":
                    Console.Write("Write student ID: ");
                    string id = Console.ReadLine() ?? "";
                    service.DeleteStudent(id);
                    Console.WriteLine("*** Deleted if exist ***");
                    break;

                case "4":
                    Console.WriteLine($"Number of students in hostel and 3 course: {service.CountMale3rdDorm()}");
                    break;
            }
        }
    }
}
