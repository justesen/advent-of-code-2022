using System;
using System.Linq;
using System.Collections.Generic;

using Pathfinding;


namespace AdventOfCode
{
    class December14 {
        static void Main(string[] args) {
            var cave = Parse();
            var lowest_rock = cave.Select(p => p.y).Max();

            var res1 = FallingSand(
                new HashSet<Point>(cave),
                p => p.y > lowest_rock,
                Int32.MaxValue);
            Console.WriteLine(res1);

            var res2 = FallingSand(
                new HashSet<Point>(cave),
                p => p.x == 500 && p.y == 0,
                lowest_rock + 2
            );
            Console.WriteLine(res2 + 1);
        }

        static HashSet<Point> Parse() {
            var input = new HashSet<Point>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                var points = line
                    .Split(" -> ")
                    .Select(s => {
                        var tokens = s.Split(",");
                        return new Point(Int32.Parse(tokens[0]), Int32.Parse(tokens[1]));
                    })
                    .ToArray();

                for (int i = 0; i < points.Length - 1; i++) {
                    if (points[i].x > points[i+1].x) {
                        for (int x = points[i].x; x >= points[i+1].x; x--) {
                            input.Add(new Point(x, points[i].y));
                        }
                    } else if (points[i].x < points[i+1].x) {
                        for (int x = points[i].x; x <= points[i+1].x; x++) {
                            input.Add(new Point(x, points[i].y));
                        }
                    } else if (points[i].y < points[i+1].y) {
                        for (int y = points[i].y; y <= points[i+1].y; y++) {
                            input.Add(new Point(points[i].x, y));
                        }
                    } else {
                        for (int y = points[i].y; y >= points[i+1].y; y--) {
                            input.Add(new Point(points[i].x, y));
                        }
                    }
                }
            }
            return input;
        }

        static int FallingSand(HashSet<Point> cave, Predicate<Point> stop, int floor) {
            for (int units = 0; ; units++) {
                var p = new Point(500, 0);
                var falling = true;

                while (falling) {
                    falling = false;
                    var candidates = new List<Point>() {
                        new Point(p.x, p.y + 1),
                        new Point(p.x - 1, p.y + 1),
                        new Point(p.x + 1, p.y + 1),
                    };

                    foreach (var q in candidates) {
                        if (!cave.Contains(q) && q.y < floor) {
                            p = q;
                            falling = true;
                            break;
                        }
                    }
                    if (stop(p)) {
                        return units;
                    }
                }
                cave.Add(p);
            }
        }
    }
}
