using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December1 {
        static void Main(string[] args) {
            var input = Parse();

            var res1 = input.Select(elf => elf.Sum()).Max();
            Console.WriteLine(res1);

            var res2 = input.Select(elf => elf.Sum()).OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine(res2);
        }

        static List<List<int>> Parse() {
            var input = new List<List<int>>();
            var current_elf = new List<int>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                if (line.Trim() == "") {
                    input.Add(current_elf);
                    current_elf = new List<int>();
                } else {
                    current_elf.Add(Int32.Parse(line));
                }
            }
            return input;
        }
    }
}
