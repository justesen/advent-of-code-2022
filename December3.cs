using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December3 {
        static void Main(string[] args) {
            var input = Parse();

            var res1 = input
                    .Select(r => (r.Substring(0, r.Length/2), r.Substring(r.Length/2, r.Length/2)))
                    .Select(p => p.Item1.Intersect(p.Item2).ElementAt(0))
                    .Select(v => v - (v >= 'a' ? 'a' - 1 : 'A' - 27))
                    .Sum();
            Console.WriteLine(res1);

            var res2 = input
                    .Select((r, i) => (r, i))
                    .GroupBy(ri => ri.Item2 / 3)
                    .Select(g => g.Select(r => r.Item1).ToArray())
                    .Select(g => g[0].Intersect(g[1]).Intersect(g[2]).ElementAt(0))
                    .Select(v => v - (v >= 'a' ? 'a' - 1 : 'A' - 27))
                    .Sum();
            Console.WriteLine(res2);
        }

        static List<string> Parse() {
            var input = new List<string>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                input.Add(line);
            }
            return input;
        }
    }
}
