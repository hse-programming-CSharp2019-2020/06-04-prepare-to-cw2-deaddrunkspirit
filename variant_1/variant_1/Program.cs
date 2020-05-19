using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace variant_1
{
    public class Program
    {
        public static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            string path = "dictionary.txt";
            try
            {
                int n = GetInt();

                File.WriteAllText(path, "");
                string[] lines = new string[n];
                for (int i = 0; i < n; i++)
                    lines[i] = GetString();

                File.AppendAllLines(path, lines);

                List<Pair<string, string>> lst = GetPairs(path);
                Dictionary dict = new Dictionary(lst);

                dict.MySerialize("out.bin");
                Dictionary dictionary2 = Dictionary.MyDeserialize("out.bin");

                foreach (var el in dictionary2)
                    Console.WriteLine(el);

                Console.WriteLine("Пары русских слов на а");

                foreach (var element in dictionary2.MyEnumerator('а'))
                    Console.WriteLine(element);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static int GetInt(string message = "Кол-во слов в словаре: ")
        {
            int number = 0;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
                Console.WriteLine("Введите целое положительное число");
            return number;
        }

        public static List<Pair<string, string>> GetPairs(string path)
        {
            List<Pair<string, string>> res = new List<Pair<string, string>>();
            string[] lines = File.ReadAllLines(path);
            foreach (string s in lines)
            {
                string w1 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string w2 = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                Pair<string, string> p = new Pair<string, string>(w1, w2);
                res.Add(p);
            }

            return res;
        }

        public static string GetString()
        {
            Console.Write("Введите русское слово: ");
            string wordRu = Console.ReadLine();

            Console.Write("Введите английское слово: ");
            string wordEn = Console.ReadLine();

            return (wordRu + " " + wordEn);
        }
    }
}
