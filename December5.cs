using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December5 {
        static void Main(string[] args) {
            // Don't wanna bother with parsing the stack layout
            var stacksOrig = new Stack<char>[] {
                new Stack<char>("ZJNWPS"),
                new Stack<char>("GST"),
                new Stack<char>("VQRLH"),
                new Stack<char>("VSTD"),
                new Stack<char>("QZTDBMJ"),
                new Stack<char>("MWTJDCZL"),
                new Stack<char>("LPMWGTJ"),
                new Stack<char>("NGMTBFQH"),
                new Stack<char>("RDGCPBQW"),
            };
            var moves = Parse();

            // Part 1
            // Copy stacks before manipulating them
            var stacks1 = stacksOrig.Select(s => new Stack<char>(s.Reverse().ToArray())).ToArray();

            foreach (Move move in moves) {
                for (int i = 0; i < move.numCrates; i++) {
                    char crate = stacks1[move.from].Pop();
                    stacks1[move.to].Push(crate);
                }
            }

            stacks1.ToList().ForEach(s => Console.Write(s.Peek()));
            Console.WriteLine();

            // Part 2
            var stacks2 = stacksOrig;

            foreach (Move move in moves) {
                var tmp = new Stack<char>();

                foreach (int value in Enumerable.Range(0,  move.numCrates)) {
                    tmp.Push(stacks2[move.from].Pop());
                }
                foreach (int value in Enumerable.Range(0,  move.numCrates)) {
                    stacks2[move.to].Push(tmp.Pop());
                }
            }

            stacks2.ToList().ForEach(s => Console.Write(s.Peek()));
            Console.WriteLine();
        }

        static List<Move> Parse() {
            var input = new List<Move>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                var tokens = line.Split();
                var numCrates = Int32.Parse(tokens[1]);
                var from = Int32.Parse(tokens[3]) - 1;
                var to = Int32.Parse(tokens[5]) - 1;
                var r2 = tokens[1].Split("-");
                input.Add(new Move(numCrates, from, to));
            }
            return input;
        }
    }

    class Move {
        public readonly int numCrates;
        public readonly int from;
        public readonly int to;

        public Move(int numCrates, int from, int to) {
            this.numCrates = numCrates;
            this.from = from;
            this.to = to;
        }
    }
}
