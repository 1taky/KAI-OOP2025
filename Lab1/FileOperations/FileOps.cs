using System;
using System.Text;
using System.IO;


namespace FileOperations
{

public class FileOps
{

        public void WriteToFile(string[] data, string name)
        {
            using (FileStream fs = new FileStream($@"../../../{name}.txt", FileMode.Append, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (var line in data)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine("};");
            }
        }

        public string ReadFromFile(string name)
        {
            if (!File.Exists($@"../../../{name}.txt"))
            {
                return string.Empty;
            }
            using (FileStream fs = new FileStream($@"../../../{name}.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public void EditFile(string[] data, string name)
        {
            using (FileStream fs = new FileStream($@"../../../{name}.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                foreach (var line in data)
                {
                    writer.WriteLine(line);
                }
                writer.WriteLine("};");
            }
        }

        public void ClearFile(string name)
        {
            File.WriteAllText($@"../../../{name}.txt", string.Empty);
        }
    }
}