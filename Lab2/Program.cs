using System;
using System.Collections;
using System.Collections.Generic;
using Lab2;

class Program
{
    static void Main()
    {
        MyString[] array = {
            new MyString("apple"),
            new MyString("banana"),
            new MyString("kiwi")
        };

        Console.WriteLine("Масив:");
        foreach (MyString s in array)
            Console.WriteLine(s);

        ArrayList list = new ArrayList()
        {
            new MyString("dog"),
            new MyString("cat")
        };
        list.Add(new MyString("mouse"));
        list.RemoveAt(1);
        Console.WriteLine("\nArrayList:");
        foreach (MyString s in list)
        Console.WriteLine(s);

        List<MyString> genericList = new List<MyString>
        {
            new MyString("book"),
            new MyString("notebook"),
            new MyString("pen")
        };

        genericList.Add(new MyString("pencil"));
        genericList.RemoveAt(0);
        genericList[1].ReplaceSubstring("pen", "marker");
        
            
        Console.WriteLine("\nList<MyString>:");
        foreach (MyString s in genericList)
            Console.WriteLine(s);

        MyString? found = genericList.FirstOrDefault(s => s.ContainsSubstring("mark"));
        Console.WriteLine($"\nЗнайдено: {found}");

        BinaryTreeDemo.Run();
    }
}
