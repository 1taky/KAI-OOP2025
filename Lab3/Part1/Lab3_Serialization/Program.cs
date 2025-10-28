using System.Text.Json;
using System.Xml.Serialization;
using MemoryPack;

namespace Lab3_Serialization
{
    class Program
    {
        static void Main()
        {
            StringEntity[] array = {
                new("Hello"),
                new("World"),
                new("Serialization"),
                new("Testing"),
                new("Moment")
            };

            Console.WriteLine("=== Array data ===");
            foreach (StringEntity s in array)
                Console.WriteLine(s);

            //JSON
            string jsonPath = "../../../../Results/strings.json";
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(array, new JsonSerializerOptions { WriteIndented = true }));

            StringEntity[] fromJson = JsonSerializer.Deserialize<StringEntity[]>(File.ReadAllText(jsonPath));
            Console.WriteLine($"\n*** JSON saved {jsonPath.Substring(12)} ***");
            Console.WriteLine($"Read from {jsonPath.Substring(12)}:");
            foreach (StringEntity s in fromJson!) Console.WriteLine(s);

            // XML
            string xmlPath = "../../../../Results/strings.xml";
            var xml = new XmlSerializer(typeof(StringEntity[]));
            using (var fs = File.Create(xmlPath))
                xml.Serialize(fs, array);
            Console.WriteLine($"\n*** XML saved {xmlPath.Substring(12)} ***");

            using var fs2 = File.OpenRead(xmlPath);
            var fromXml = (StringEntity[]?)xml.Deserialize(fs2);
            Console.WriteLine($"Read from {xmlPath.Substring(12)}:");
            foreach (StringEntity s in fromXml!) Console.WriteLine(s);

            //MemoryPack
            string memPath = "../../../../Results/strings.bin";
            File.WriteAllBytes(memPath, MemoryPackSerializer.Serialize(array));

            var fromMemoryPack = MemoryPackSerializer.Deserialize<StringEntity[]>(File.ReadAllBytes(memPath));
            Console.WriteLine($"\n*** MemoryPack saved {memPath.Substring(12)} ***");
            Console.WriteLine($"Read from {memPath.Substring(12)}:");
            foreach (StringEntity s in fromMemoryPack!) Console.WriteLine(s);

            //Custom
            string customPath = "../../../../Results/strings_custom.txt";
            using (var writer = new StreamWriter(customPath))
            {
                foreach (StringEntity s in array)
                    writer.WriteLine($"{s.Value}|{s.Length}");
            }
            Console.WriteLine($"\n*** CustomText saved {customPath.Substring(12)} ***");

            var customList = new List<StringEntity>();
            foreach (string line in File.ReadAllLines(customPath))
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                    customList.Add(new StringEntity(parts[0]));
            }
            Console.WriteLine($"Read from {customPath.Substring(12)}:");
            foreach (StringEntity s in customList) Console.WriteLine(s);

            var list = new List<StringEntity>(array);
            string listJson = JsonSerializer.Serialize(list);
            Console.WriteLine($"\n=== Compare ===");
            Console.WriteLine($"Array JSON bytes: {File.ReadAllBytes(jsonPath).Length}");
            Console.WriteLine($"List JSON bytes: {System.Text.Encoding.UTF8.GetBytes(listJson).Length}");
            Console.WriteLine($"MemoryPack bytes: {File.ReadAllBytes(memPath).Length}");
        }
    }
}
