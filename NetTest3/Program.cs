using System;
using System.Collections.Generic;
using System.Linq;

namespace NetTest3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task No.3!");
        }

        public static IEnumerable<IEnumerable<string>> OnlyBigCollections(List<IEnumerable<string>> toFilter)
        {
            Func<IEnumerable<string>, bool> predicate =
                list => list.Take(6)?.Count() > 5;

            return toFilter.Where(predicate);
        }
    }
}
