using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Lab1.Models;
using Entities;
using Lab1;

namespace FileOperations
{
    public class DatabaseOperations
    {
        private Student[] students = new Student[10];
        private Seller[] sellers = new Seller[10];
        private Gardener[] gardeners = new Gardener[10];
        private int studentCount, sellerCount, gardenerCount = 0;
        private FileOps fileOps = new FileOps();

        public void AddStudent(Student stud)
        {
            students[studentCount++] = stud;
            fileOps.WriteToFile(stud.MakeAsJSONFormat(), "Students");
        }

        public void AddSeller(Seller sel)
        {
            for (int i = 0; i < sellerCount; i++)
                if (sellers[i].FirstName == sel.FirstName && sellers[i].LastName == sel.LastName)
                    return;
            sellers[sellerCount++] = sel;
            fileOps.WriteToFile(sel.MakeAsJSONFormat(), "Sellers");
        }

        public void AddGardener(Gardener gar)
        {
            for (int i = 0; i < gardenerCount; i++)
                if (gardeners[i].FirstName == gar.FirstName && gardeners[i].LastName == gar.LastName)
                    return;
            gardeners[gardenerCount++] = gar;
            fileOps.WriteToFile(gar.MakeAsJSONFormat(), "Gardeners");
        }

        public void LoadStudents()
        {
            string data = fileOps.ReadFromFile("Students");
            if (string.IsNullOrEmpty(data)) return;
            string[] lines = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            while (i < lines.Length)
            {
                string typeLine = lines[i++];
                string firstName = "", lastName = "", home = "";
                int course = 0, studNum = 0;
                Gender gender = Gender.Male;
                while (i < lines.Length && lines[i].Trim() != "};")
                {
                    string line = lines[i++].Trim();
                    if (line.StartsWith("\"firstname\"")) firstName = line.Split('"')[3];
                    else if (line.StartsWith("\"lastname\"")) lastName = line.Split('"')[3];
                    else if (line.StartsWith("\"course\"")) course = int.Parse(line.Split('"')[3]);
                    else if (line.StartsWith("\"studentID\"")) studNum = int.Parse(line.Split('"')[3].Substring(2));
                    else if (line.StartsWith("\"gender\"")) gender = (Gender)Enum.Parse(typeof(Gender), line.Split('"')[3], true);
                    else if (line.StartsWith("\"homeplace\"")) home = line.Split('"')[3];
                }
                i++;
                bool exists = false;
                for (int j = 0; j < studentCount; j++)
                    if (students[j].StudId.FullID == $"KB{studNum:D6}") { exists = true; break; }
                if (!exists)
                    students[studentCount++] = new Student(firstName, lastName, course, new StudentId(studNum), gender, home);
            }
        }

        public void LoadSellers()
        {
            string data = fileOps.ReadFromFile("Sellers");
            if (string.IsNullOrEmpty(data)) return;
            string[] lines = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            while (i < lines.Length)
            {
                string typeLine = lines[i++];
                string firstName = "", lastName = "", item = "";
                Gender gender = Gender.Male;
                while (i < lines.Length && lines[i].Trim() != "};")
                {
                    string line = lines[i++].Trim();
                    if (line.StartsWith("\"firstname\"")) firstName = line.Split('"')[3];
                    else if (line.StartsWith("\"lastname\"")) lastName = line.Split('"')[3];
                    else if (line.StartsWith("\"gender\"")) gender = (Gender)Enum.Parse(typeof(Gender), line.Split('"')[3], true);
                    else if (line.StartsWith("\"goods\"")) item = line.Split('"')[3];
                }
                i++;
                bool exists = false;
                for (int j = 0; j < sellerCount; j++)
                    if (sellers[j].FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                        sellers[j].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                        sellers[j].Item.Equals(item, StringComparison.OrdinalIgnoreCase)) { exists = true; break; }
                if (!exists)
                    sellers[sellerCount++] = new Seller(firstName, lastName, gender, item);
            }
        }

        public void LoadGardeners()
        {
            string data = fileOps.ReadFromFile("Gardeners");
            if (string.IsNullOrEmpty(data)) return;
            string[] lines = data.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            while (i < lines.Length)
            {
                string typeLine = lines[i++];
                string firstName = "", lastName = "", plant = "";
                Gender gender = Gender.Male;
                while (i < lines.Length && lines[i].Trim() != "};")
                {
                    string line = lines[i++].Trim();
                    if (line.StartsWith("\"firstname\"")) firstName = line.Split('"')[3];
                    else if (line.StartsWith("\"lastname\"")) lastName = line.Split('"')[3];
                    else if (line.StartsWith("\"gender\"")) gender = (Gender)Enum.Parse(typeof(Gender), line.Split('"')[3], true);
                    else if (line.StartsWith("\"plant\"")) plant = line.Split('"')[3];
                }
                i++;
                bool exists = false;
                for (int j = 0; j < gardenerCount; j++)
                    if (gardeners[j].FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                        gardeners[j].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                        gardeners[j].Plant.Equals(plant, StringComparison.OrdinalIgnoreCase)) { exists = true; break; }
                if (!exists)
                    gardeners[gardenerCount++] = new Gardener(firstName, lastName, gender, plant);
            }
        }

        public void ShowAll()
        {
            LoadStudents();
            LoadSellers();
            LoadGardeners();
            Console.WriteLine("\n--- STUDENTS ---");
            for (int i = 0; i < studentCount; i++)
                Console.WriteLine($"{students[i].FirstName} {students[i].LastName} ({students[i].StudId.FullID}) Course: {students[i].Course} Live in: {students[i].HomePlace}");
            Console.WriteLine("\n--- SELLERS ---");
            for (int i = 0; i < sellerCount; i++)
                Console.WriteLine($"{sellers[i].FirstName} {sellers[i].LastName} ({sellers[i].Item})");
            Console.WriteLine("\n--- GARDENERS ---");
            for (int i = 0; i < gardenerCount; i++)
                Console.WriteLine($"{gardeners[i].FirstName} {gardeners[i].LastName} ({gardeners[i].Plant})");
        }

        public void MalesFromHostelAndOnThirdCourse()
        {
            int count = 0;
            Console.WriteLine("\n--- Students in hostel and on 3rd course ---");
            for (int i = 0; i < studentCount; i++)
                if (students[i].Course == 3 && students[i].IsFromHostel() && students[i].Gender == Gender.Male)
                {
                    count++;
                    Console.WriteLine($"{students[i].FirstName} {students[i].LastName} ({students[i].StudId.FullID}), Home: {students[i].HomePlace}");
                }
            Console.WriteLine($"\nTotal students living in hostel and on 3rd course: {count}");
        }

        public Student? FindStudentByLastName(string lastName)
        {
            LoadStudents();
            for (int i = 0; i < studentCount; i++)
                if (students[i].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                    return students[i];
            return null;
        }

        public void DeleteFromDatabase(string lastName)
        {
            LoadStudents();
            LoadSellers();
            LoadGardeners();

            Student[] beforeDeletingStudents = new Student[studentCount];
            Seller[] beforeDeletingSellers = new Seller[sellerCount];
            Gardener[] beforeDeletingGardeners = new Gardener[gardenerCount];

            for (int i = 0; i < studentCount; i++) beforeDeletingStudents[i] = students[i];
            for (int i = 0; i < sellerCount; i++) beforeDeletingSellers[i] = sellers[i];
            for (int i = 0; i < gardenerCount; i++) beforeDeletingGardeners[i] = gardeners[i];

            fileOps.ClearFile("Students");
            fileOps.ClearFile("Sellers");
            fileOps.ClearFile("Gardeners");

            students = new Student[10];
            sellers = new Seller[10];
            gardeners = new Gardener[10];
            studentCount = sellerCount = gardenerCount = 0;

            for (int i = 0; i < beforeDeletingStudents.Length; i++)
                if (beforeDeletingStudents[i] != null && !beforeDeletingStudents[i].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                    AddStudent(beforeDeletingStudents[i]);

            for (int i = 0; i < beforeDeletingSellers.Length; i++)
                if (beforeDeletingSellers[i] != null && !beforeDeletingSellers[i].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                    AddSeller(beforeDeletingSellers[i]);

            for (int i = 0; i < beforeDeletingGardeners.Length; i++)
                if (beforeDeletingGardeners[i] != null && !beforeDeletingGardeners[i].LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                    AddGardener(beforeDeletingGardeners[i]);
        }
    }
}
