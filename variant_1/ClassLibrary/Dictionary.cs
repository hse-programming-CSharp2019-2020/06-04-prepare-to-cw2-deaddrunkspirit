using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    public class Dictionary : IEnumerable
    {
        private static Random rnd = new Random();

        private int locale;
        private List<Pair<string, string>> words;

        public Dictionary(List<Pair<string, string>> words)
        {
            this.words = words;
            locale = rnd.Next(0, 2);
        }

        public Dictionary()
            => words = new List<Pair<string, string>>();

        public void Add(Pair<string, string> addition)
        {
            words.Add(addition);
        }

        public void Add(string word1, string word2)
        {
            Pair<string, string> newPair = new Pair<string, string>(word1, word2);
            words.Add(newPair);
        }
        public IEnumerator GetEnumerator()
        {
            words.Sort();
            return words.GetEnumerator();
        }


        public IEnumerable MyEnumerator(char letter)
        {

            if (letter < 'я' + 1 && letter > 'а' - 1)
            {
                var lst = new List<Pair<string, string>>();
                foreach (var word in words)
                    if (word.Item1[0] == letter) 
                        lst.Add(word);

                foreach (var element in lst) 
                    yield return element;
            }
            else
            {
                words.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                foreach (var w in words)
                    yield return w;
            }
        }

        public void MySerialize(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
        }

        public static Dictionary MyDeserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary)formatter.Deserialize(fs);
            }
        }


    }
}
