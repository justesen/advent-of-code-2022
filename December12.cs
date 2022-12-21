using System;
using System.Linq;
using System.Collections.Generic;

using Pathfinding;


namespace AdventOfCode
{
    class December12 {
        static void Main(string[] args) {
            var (start, goal, map) = Parse();

            var res1 = BFS<Point>.Search(start, goal, Neighbors(map)).Count - 1;
            Console.WriteLine(res1);

            int res2 = Int32.MaxValue;
            for (int y = 0; y < map.Length; y++) {
                for (int x = 0; x < map[y].Length; x++) {
                    if (map[y][x] == 'a') {
                        var path = BFS<Point>.Search(new Point(x, y), goal, Neighbors(map));

                        if (path != null) {
                            res2 = Math.Min(path.Count - 1, res2);
                        }
                    }
                }
            }
            Console.WriteLine(res2);
        }

        static (Point start, Point goal, char[][] map) Parse() {
            var input = new List<char[]>();
            string line = "";
            int row = 0;
            Point start = null;
            Point goal = null;

            while ((line = Console.ReadLine()) != null) {
                int start_col = line.IndexOf('S');
                int goal_col = line.IndexOf('E');

                if (start_col >= 0) {
                    start = new Point(start_col, row);
                }
                if (goal_col >= 0) {
                    goal = new Point(goal_col, row);
                }
                input.Add(line.Replace("S", "a").Replace("E", "z").ToArray());
                row++;
            }
            return (start, goal, input.ToArray());
        }

        static Func<Point, IEnumerable<Point>> Neighbors(char[][] map) {
            int rows = map.Length;
            int cols = map[0].Length;

            return (Point p) => {
                return new List<Point>() {
                    new Point(p.x - 1, p.y),
                    new Point(p.x + 1, p.y),
                    new Point(p.x, p.y - 1),
                    new Point(p.x, p.y + 1),
                }.Where(q => q.x >= 0 && q.y >= 0 && q.x < cols && q.y < rows && map[q.y][q.x] - map[p.y][p.x] <= 1);
            };
        }
    }
}
