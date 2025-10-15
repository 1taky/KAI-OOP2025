using System;
using System.Text.RegularExpressions;
using FileOperations;
using Lab1.Models;

namespace Lab1
{
    public class ConsoleMenu
    {
        private DatabaseOperations db = new DatabaseOperations();

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1. Add new person");
                Console.WriteLine("2. Show all");
                Console.WriteLine("3. Find student by last name");
                Console.WriteLine("4. Show males who lives in hostel and on 3-rd course");
                Console.WriteLine("5. Delete from DB");
                Console.WriteLine("0. Exit");
                Console.Write("Select: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddHuman(); break;
                    case "2": db.ShowAll(); break;
                    case "3": SearchStudent(); break;
                    case "4": db.MalesFromHostelAndOnThirdCourse(); break;
                    case "5": 
                        Console.WriteLine("\nEnter last name to delete user: ");
                        string lName = Console.ReadLine();
                        db.DeleteFromDatabase(lName);
                        Console.WriteLine("Successfully deleted");
                        break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Invalid input"); break;
                }
            }
        }

        private void AddHuman()
        {
            Console.WriteLine("Who to add: 1 - Student, 2 - Seller, 3 - Gardener");
            string type = Console.ReadLine();

            Console.Write("First name: ");
            string fn = Console.ReadLine();
            Console.Write("Last name: ");
            string ln = Console.ReadLine();
            Console.Write("Gender (Male/Female): ");
            Gender gdr = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine(), true);

            switch (type)
            {
                case "1":
                    Console.Write("Course: ");
                    int c = int.Parse(Console.ReadLine());
                    Console.Write("Student number: ");
                    string num = Console.ReadLine();
                    string studIdPatern = @"^\d{6}$";
                    if(!Regex.IsMatch(num, studIdPatern))
                    {
                        while (!Regex.IsMatch(num, studIdPatern))
                        {
                            Console.Write("incorrect format. (eg: 123456). please try again: ");
                            num = Console.ReadLine();
                        }
                    }
                    Console.Write("Home place: ");
                    string home = Console.ReadLine();
                    db.AddStudent(new Student(fn, ln, c, new StudentId(int.Parse(num)), gdr, home));
                    break;

                case "2":
                    Console.Write("Goods: ");
                    string item = Console.ReadLine();
                    db.AddSeller(new Seller(fn, ln, gdr, item));
                    break;

                case "3":
                    Console.Write("Plant: ");
                    string plant = Console.ReadLine();
                    db.AddGardener(new Gardener(fn, ln, gdr, plant));
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        private void SearchStudent()
        {
            Console.Write("Enter last name: ");
            string last = Console.ReadLine();
            var st = db.FindStudentByLastName(last);
            if (st != null)
                Console.WriteLine($"Found student: {st.FirstName} {st.LastName} ({st.StudId.FullID})");
            else
                Console.WriteLine("Not found.");
        }
    }
}