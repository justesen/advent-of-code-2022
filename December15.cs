using System;
using System.Linq;
using System.Collections.Generic;

using Pathfinding;


namespace AdventOfCode
{
    class December15 {
        static void Main(string[] args) {
            var (map, nearest_beacon) = Parse();

            var res1 = CoveredSpots(map, nearest_beacon, 2000000);
            Console.WriteLine(res1);

            var p = UncoveredSpot(map, nearest_beacon, 4000000, 4000000);
            ulong res2 = (ulong) p.x * 4000000 + (ulong) p.y;
            Console.WriteLine(res2);
        }

        static (Dictionary<Point, char>, Dictionary<Point, Point>) Parse() {
            var map = new Dictionary<Point, char>();
            var nearest_beacon = new Dictionary<Point, Point>();
            string line = "";

            while ((line = Console.ReadLine()) != null) {
                var tokens = line.Split();
                Point s = new Point(
                    Int32.Parse(tokens[2].Substring(2, tokens[2].Length - 3)),
                    Int32.Parse(tokens[3].Substring(2, tokens[3].Length - 3))
                );
                Point b = new Point(
                    Int32.Parse(tokens[8].Substring(2, tokens[8].Length - 3)),
                    Int32.Parse(tokens[9].Substring(2, tokens[9].Length - 2))
                );
                map[s] = 'S';
                map[b] = 'B';
                nearest_beacon[s] = b;
            }
            return (map, nearest_beacon);
        }

        static int CoveredSpots(
                Dictionary<Point, char> map,
                Dictionary<Point, Point> nearest_beacon,
                int y) {
            var covered = new HashSet<Point>();

            foreach (var entry in nearest_beacon) {
                var s = entry.Key;
                var b = entry.Value;
                int dist = ManhattanDist(s, b);
                int y_dist = Math.Abs(y - s.y);

                if (y_dist < dist) {
                    for (int x = s.x - dist + y_dist; x <= s.x + dist - y_dist; x++) {
                        var p = new Point(x, y);

                        if (!map.ContainsKey(p)) {
                            covered.Add(p);
                        }
                    }
                }
            }
            return covered.Count;
        }

        static Point UncoveredSpot(
                Dictionary<Point, char> map,
                Dictionary<Point, Point> nearest_beacon,
                int x_limit, int y_limit) {
            foreach (var entry in nearest_beacon) {
                var s = entry.Key;
                var b = entry.Value;
                int dist = ManhattanDist(s, b);

                // Since there's only one empty spot, it must be just on the
                // edge of what's `dist` away from `s`
                for (int i = 0; i <= 2*(dist + 1); i++) {
                    int x = s.x - dist - 1 + i;
                    int y1 = s.y + (i <= dist + 1 ? i : (i - 2*(i - (dist + 1))));
                    int y2 = s.y - (i <= dist + 1 ? i : (i - 2*(i - (dist + 1))));
                    Point p = new Point(x, y1);
                    Point q = new Point(x, y2);

                    foreach (var r in new List<Point> {p, q}) {
                        if (0 <= r.x && r.x <= x_limit && 0 <= r.y && r.y <= y_limit) {
                            if (Clear(nearest_beacon, r)) {
                                return r;
                            }
                        }
                    }
                }
            }
            return new Point(-1, -1);
        }

        static bool Clear(Dictionary<Point, Point> nearest_beacon, Point p) {
            foreach (var entry in nearest_beacon) {
                var s = entry.Key;
                var b = entry.Value;
                if (ManhattanDist(s, p) <= ManhattanDist(s, b)) {
                    return false;
                }
            }
            return true;
        }

        static int ManhattanDist(Point p, Point q) {
            return Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y);
        }
    }
}
