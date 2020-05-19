using System;
using System.Linq;

namespace Library
{
    [Serializable]
    public class Street
    {
        public string name { get; set; }

        public int[] houses { get; set; }

        public Street(string name, int[] houseNumbers)
        {
            if (houseNumbers == null || name == null)
                throw new ArgumentNullException();
            this.name = name;
            houses = houseNumbers;
        }

        public static int operator ~(Street street)
            => street.houses.Length;

        public static bool operator !(Street street)
        {
            var housesWithSeven = from house in street.houses where house.ToString().Contains("7") select house;
            if (housesWithSeven.Count() >= 1)
                return true;
            return false;
        }

        public override string ToString()
        {
            if (houses == null)
                throw new ArgumentNullException("Дома отсутствуют");
            string result = name;
            foreach (var house in houses)
                result += $" {house}";

            return result;
        }
    }
}
