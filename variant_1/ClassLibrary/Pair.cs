using System;
using System.Linq;

namespace ClassLibrary
{
    [Serializable]
    public class Pair<T, U> : IComparable where T : IComparable
    {
        public T Item1 { get; }
        public U Item2 { get; }

        public Pair(T item1, U item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public int CompareTo(object obj)
            => ((Pair<T, U>)obj).Item1.CompareTo(Item1);

        public override string ToString()
            => $"Item1: {Item1}, Item2: {Item2}";
    }
}
