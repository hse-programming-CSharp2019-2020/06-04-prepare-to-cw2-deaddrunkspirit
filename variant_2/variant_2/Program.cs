using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace variant_2
{
    public class Program
    {
        private static Random rnd = new Random();

        public static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            char sep = Path.DirectorySeparatorChar;
            string path = $"data.txt";
            int N = GetInt("Введите N: ");

            List<Street> streets = new List<Street>();
            if (CheckStreetDataFile(path))
            {
                var streetsData = File.ReadAllLines(path);

                foreach (var street in streetsData)
                {
                    string[] str = street.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int temp = 0;
                    var тгьиукы = (from t in str
                                where int.TryParse(t, out temp)
                                select temp).ToList();
                    try
                    {

                        streets.Add(new Street(str[0], тгьиукы.ToArray()));
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }
            else
                FillStreetData(streets, N);

            if (streets.Count < N)
                FillStreetData(streets, N - streets.Count);

            foreach (var street in streets)
                Console.WriteLine(street);

            SerializeStreets("out.txt", streets);
        }

        private static void SerializeStreets(string path, List<Street> streets)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Street>));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, streets);
                Console.WriteLine("Сериализация завершена");
            }
        }

        private static void FillStreetData(List<Street> streets, int n)
        {
            for (int i = 0; i < n; i++)
                streets.Add(new Street(GenerateName(), GenerateIntArr()));
        }

        static string GenerateName()
        {
            string result = $"{(char)rnd.Next('A', 'Z' + 1)}";
            int length = rnd.Next(3, 8);
            for (int i = 0; i < length; i++)
                result += (char)rnd.Next('a', 'z' + 1);
            
            return result;
        }

        static int[] GenerateIntArr()
        {
            var numbers = new List<int>();
            int length = rnd.Next(1, 11);

            for (int i = 0; i < length; i++)
                numbers.Add(rnd.Next(1, 101));

            return numbers.ToArray();
        }

        static int GetInt(string message)
        {
            int number = 0;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > 1000)
                Console.WriteLine("Введите целое число в границах [1, 1000]");
            
            return number;
        }

        static bool CheckStreetDataFile(string path)
        {
            if (!File.Exists(path))
                return false;

            var streetsData = File.ReadAllLines(path);

            if (streetsData.Length == 0)
                return false;
            bool validData = true;

            foreach (var street in streetsData)
            {
                string[] str = street.Split(new char[] { ' ' });
                if (str.Length < 2)
                    return false;
                int temp = 0;
                var nums = (from t in str
                            where int.TryParse(t, out temp)
                            select t).ToList();

                if (nums.Count != str.Length - 1)
                {
                    validData = false;
                    break;
                }
            }

            return validData;
        }
    }
}
