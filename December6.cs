using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December6 {
        static void Main(string[] args) {
            var input = Console.ReadLine();
            Console.WriteLine(FindPosOfDistinctChars(input, 4));
            Console.WriteLine(FindPosOfDistinctChars(input, 14));
        }

        static int FindPosOfDistinctChars(string input, int n) {
            char[] mostRecent = new char[n];

            for (int i = 0; i < input.Length; i++) {
                mostRecent[i % mostRecent.Length] = input[i];
                if (i >= mostRecent.Length - 1 && mostRecent.Distinct().Count() == mostRecent.Length) {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
