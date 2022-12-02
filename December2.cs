using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December2 {
        static void Main(string[] args) {
            var input = Parse();

            var res1 = input.Select(p => p.Eval()).Sum();
            Console.WriteLine(res1);

            var res2 = input.Select(p => p.EvalStrategy()).Sum();
            Console.WriteLine(res2);
        }

        static List<RPSPair> Parse() {
            var input = new List<RPSPair>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                string[] tokens = line.Split();
                input.Add(new RPSPair(tokens[0], tokens[1]));
            }
            return input;
        }
    }

    class RPSPair {
        public readonly int s;
        public readonly int t;

        public RPSPair(string s, string t) {
            this.s = s[0] - 'A';
            this.t = t[0] - 'X';
        }

        public int Eval() {
            int shape_score = t + 1;
            int round_score = 0;

            if (s == t) {
                round_score = 3;
            } else if ((s == 0 && t == 1)
                    || (s == 1 && t == 2)
                    || (s == 2 && t == 0) ) {
                round_score = 6;
            }

            return shape_score + round_score;
        }

        static int Mod(int a, int n) {
            return ((a % n) + n) % n;
        }

        public int EvalStrategy() {
            int shape_score = 0;
            int round_score = t * 3;

            if (t == 0) {
                shape_score = Mod(s - 1, 3);
            } else if (t == 1) {
                shape_score = s;
            } else {
                shape_score = Mod(s + 1, 3);
            }

            return shape_score + 1 + round_score;
        }
    }
}
