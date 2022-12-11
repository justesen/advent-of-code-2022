using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December10 {
        static void Main(string[] args) {
            var instructions = Parse();

            var res1 = ComputeRegisterX(instructions)
                    .Select((x, i) => i*x)
                    .Skip(20)
                    .Where((x, i) => i % 40 == 0)
                    .Sum();
            Console.WriteLine(res1);

            DrawCRT(instructions);
        }

        static List<Instruction> Parse() {
            var input = new List<Instruction>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                string[] tokens = line.Split();
                if (tokens.Length == 1) {
                    input.Add(new Instruction(tokens[0]));
                } else {
                    input.Add(new Instruction(tokens[0], Int32.Parse(tokens[1])));
                }
            }
            return input;
        }

        static int[] ComputeRegisterX(List<Instruction> instructions) {
            int[] x = new int[2*instructions.Count];
            int k = 1;
            int X = 1;

            foreach (var inst in instructions) {
                k++;

                if (inst.op == "addx") {
                    x[k] = X;
                    k++;
                    X += inst.arg.GetValueOrDefault(0);
                }
                x[k] = X;
            }
            return x;
        }

        static void DrawCRT(List<Instruction> instructions) {
            int pixel = 0;
            int X = 1;

            foreach (var inst in instructions) {
                pixel = DrawPixel(pixel, X);

                if (inst.op == "addx") {
                    pixel = DrawPixel(pixel, X);
                    X += inst.arg.GetValueOrDefault(0);
                }
            }
        }

        static int DrawPixel(int pixel, int X) {
            Console.Write(Math.Abs(X - pixel) <= 1 ? "#" : ".");

            if (pixel + 1 == 40) {
                Console.WriteLine();
            }
            return (pixel + 1) % 40;
        }
    }

    class Instruction {
        public readonly string op;
        public readonly Nullable<int> arg;

        public Instruction(string op) {
            this.op = op;
            this.arg = null;
        }

        public Instruction(string op, int arg) {
            this.op = op;
            this.arg = arg;
        }
    }
}
