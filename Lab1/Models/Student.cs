using System;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Entities;
using Lab1.Models;

namespace Lab1
{

    public class Student : Human, IExtraSkill
    {
        public int Course { get; set; }
        public StudentId StudId { get; set; }
        public string HomePlace { get; set; }

        public Student(string fName, string lName, int course, StudentId studId, Gender gender, string homePlace)
         : base(fName, lName, gender)
        {
            Course = course;
            StudId = studId;
            HomePlace = homePlace;
        }

        public bool IsFromHostel()
        {
            return Regex.IsMatch(HomePlace, @"^\d+-\d+$");
        }

        public void Study()
        {
            Console.WriteLine($"{FirstName} {LastName} is studying.");
        }

        public void SleepStanding()
        {
            Console.WriteLine($"{FirstName} {LastName} student is sleeping after studying.");
        }

        public string[] MakeAsJSONFormat()
        {
            return
            [
                $"Student {FirstName}{LastName}",
                "{",
                $"\"firstname\": \"{FirstName}\",",
                $"\"lastname\": \"{LastName}\",",
                $"\"course\": \"{Course}\",",
                $"\"studentID\": \"{StudId.FullID}\",",
                $"\"gender\": \"{Gender}\",",
                $"\"homeplace\": \"{HomePlace}\"",
            ];
        }
    }
}