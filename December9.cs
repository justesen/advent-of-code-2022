using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December9 {
        static void Main(string[] args) {
            var moves = Parse();

            var res1 = VisitedCells(moves, 2).Count;
            Console.WriteLine(res1);

            var res2 = VisitedCells(moves, 10).Count;
            Console.WriteLine(res2);
        }

        static List<Move> Parse() {
            var input = new List<Move>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                string[] tokens = line.Split();
                input.Add(new Move(tokens[0][0], Int32.Parse(tokens[1])));
            }
            return input;
        }

        static HashSet<Point> VisitedCells(List<Move> moves, int knots) {
            var cells = new HashSet<Point>();
            var rope = new Point[knots];

            for (int i = 0; i < knots; i++) {
                rope[i] = new Point(0, 0);
            }

            foreach (Move m in moves) {
                for (int i = 0; i < m.count; i++) {
                    rope[0] = rope[0].Move(m.direction);

                    for (int j = 1; j < knots; j++) {
                        rope[j] = rope[j].MoveTowards(rope[j - 1]);
                    }
                    cells.Add(rope[knots - 1]);
                }
            }
            return cells;
        }

        class Point {
            public readonly int x;
            public readonly int y;

            public Point(int x, int y) {
                this.x = x;
                this.y = y;
            }

            public Point Move(char direction) {
                int dx = 0;
                int dy = 0;

                if (direction == 'U') {
                    dy = 1;
                } else if (direction == 'D') {
                    dy = -1;
                } else if (direction == 'R') {
                    dx = 1;
                } else {
                    dx = -1;
                }
                return new Point(x + dx, y + dy);
            }

            public Point MoveTowards(Point p) {
                int dx = Math.Abs(p.x - x);
                int dy = Math.Abs(p.y - y);
                int mx = dx > 1 || dx + dy > 2 ? Math.Sign(p.x - x) : 0;
                int my = dy > 1 || dx + dy > 2 ? Math.Sign(p.y - y) : 0;

                return new Point (x + mx, y + my);
            }

            public override bool Equals(object obj) {
                return obj is Point p && x == p.x && y == p.y;
            }

            public override int GetHashCode() {
                return HashCode.Combine(x, y);
            }
        }

        class Move {
            public readonly char direction;
            public readonly int count;

            public Move(char direction, int count) {
                this.direction = direction;
                this.count = count;
            }
        }
    }
}
