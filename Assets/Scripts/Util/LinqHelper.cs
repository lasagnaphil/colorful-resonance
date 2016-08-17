using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class LinqHelper
    {
        public static Random random = new Random();

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            return list.ElementAt(random.Next(list.Count()));
        }
    }
}