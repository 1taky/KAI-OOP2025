using System;

namespace Lab2;

public class BinaryTreeDemo
{
    public static void Run()
        {
            var tree = new BinaryTree<MyString>();
            tree.Insert(new MyString("car"));
            tree.Insert(new MyString("engine"));
            tree.Insert(new MyString("exhaust"));
            tree.Insert(new MyString("bi"));

            Console.WriteLine("\nPostorder обхід дерева:");
            foreach (var item in tree)
                Console.WriteLine(item);

            Console.WriteLine("\nСортування списку за довжиною:");
            var list = new List<MyString>
            {
                new MyString("hi"),
                new MyString("watermelon"),
                new MyString("cat")
            };
            list.Sort();
            foreach (var s in list)
                Console.WriteLine(s);
        }
    }

