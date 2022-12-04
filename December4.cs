using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December4 {
        static void Main(string[] args) {
            var input = Parse();

            var res1 = input
                    .Where(rs => Range.Contained(rs.Item1, rs.Item2))
                    .Count();
            Console.WriteLine(res1);

            var res2 = input
                    .Where(rs => Range.Overlapped(rs.Item1, rs.Item2))
                    .Count();
            Console.WriteLine(res2);
        }

        static List<(Range, Range)> Parse() {
            var input = new List<(Range, Range)>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                var tokens = line.Split(",");
                var r1 = tokens[0].Split("-");
                var r2 = tokens[1].Split("-");
                input.Add((new Range(Int32.Parse(r1[0]), Int32.Parse(r1[1])), new Range(Int32.Parse(r2[0]), Int32.Parse(r2[1]))));
            }
            return input;
        }
    }

    class Range {
        public readonly int a;
        public readonly int b;

        public Range(int a, int b) {
            this.a = a;
            this.b = b;
        }

        private bool Contains(Range s) {
            return a >= s.a && b <= s.b;
        }

        public static bool Contained(Range r, Range s) {
            return r.Contains(s) || s.Contains(r);
        }

        private bool Overlaps(Range s) {
            return (s.a <= a && a <= s.b)
                || (s.a <= b && b <= s.b);
        }

        public static bool Overlapped(Range r, Range s) {
            return r.Overlaps(s) || s.Overlaps(r);
        }
    }
}
