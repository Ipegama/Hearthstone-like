using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ListExtensions
    {
        private static readonly Random rng = new Random();

        public static T Random<T>(this IList<T> list)
        {
            if(list.Count > 0)
            {
                return list[UnityEngine.Random.Range(0,list.Count)];
            }
            return default;
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> GetCopy<T>(this IList<T> list)
        {
            return new List<T>(list.ToArray());
        }
    }
}
