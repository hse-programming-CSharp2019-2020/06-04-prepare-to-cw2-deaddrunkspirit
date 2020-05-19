using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SecondTask
{
    public class Program
    {
        public static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            string path = $"../../../ConsoleApp/bin/debug/out.txt";
            var streets = DeserializeStreets(path);

            var tempName = (from street in streets
                            where ~street % 2 != 0 && !street
                            select street).ToList();

            if (tempName.Count == 0)
                Console.WriteLine("Улицы не найдены");
            else
                foreach (var str in tempName)
                    Console.WriteLine(str);
        }

        private static List<Street> DeserializeStreets(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Street>));
            var streets = new List<Street>();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    streets = (List<Street>)formatter.Deserialize(stream);
                    Console.WriteLine("Десериализация завершена");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return streets;
        }
    }
}
