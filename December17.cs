using System;
using System.Linq;
using System.Collections.Generic;

using Pathfinding;


namespace AdventOfCode
{
    class December17 {
        public static readonly int TERRAIN_WIDTH = 7;
        static readonly int ROCK_TYPES = 5;

        static void Main(string[] args) {
            var wind = Parse();

            var res1 = FallRocks(wind, 2022);
            Console.WriteLine(res1);
        }

        static string Parse() {
            return Console.ReadLine().Trim();
        }

        static int FallRocks(string wind, int num_rocks) {
            int height = -1;
            int w = 0;
            var terrain = new HashSet<Point>();

            for (int i = 0; i < num_rocks; i++) {
                var rock = new Rock(i % ROCK_TYPES, height + 4);

                rock.Push(wind[w++ % wind.Length], terrain, height);
                var moving = true;

                while (moving) {
                    if (rock.CanFall(terrain, height)) {
                        rock.Fall();
                        rock.Push(wind[w++ % wind.Length], terrain, height);
                    } else {
                        moving = false;
                        terrain = rock.Stop(terrain);
                    }
                }

                height = terrain.Select(t => t.y).Max();
            }

            return height + 1;
        }
    }

    class Rock {
        public HashSet<Point> parts;

        public Rock(int type, int height) {
            if (type == 0) {
                parts = new HashSet<Point>() {
                    new Point(2, height),
                    new Point(3, height),
                    new Point(4, height),
                    new Point(5, height)
                };
            } else if (type == 1) {
                parts = new HashSet<Point>() {
                    new Point(3, height + 2),
                    new Point(2, height + 1),
                    new Point(3, height + 1),
                    new Point(4, height + 1),
                    new Point(3, height)
                };
            } else if (type == 2) {
                parts = new HashSet<Point>() {
                    new Point(4, height + 2),
                    new Point(4, height + 1),
                    new Point(2, height),
                    new Point(3, height),
                    new Point(4, height)
                };
            } else if (type == 3) {
                parts = new HashSet<Point>() {
                    new Point(2, height + 3),
                    new Point(2, height + 2),
                    new Point(2, height + 1),
                    new Point(2, height)
                };
            } else {
                parts = new HashSet<Point>() {
                    new Point(2, height + 1),
                    new Point(3, height + 1),
                    new Point(2, height),
                    new Point(3, height)
                };
            }
        }

        public bool CanFall(HashSet<Point> terrain, int height) {
            return parts.All(p =>
                0 < p.y
                && (p.y - 1 > height
                    || !terrain.Contains(new Point(p.x, p.y - 1))));
        }

        public void Fall() {
            parts = parts.Select(p => new Point(p.x, p.y - 1)).ToHashSet();
        }

        public void Push(char dir, HashSet<Point> terrain, int height) {
            int inc = (dir == '<') ? -1 : +1;

            bool canBePushed = parts.All(p =>
                0 <= p.x + inc
                && p.x + inc < December17.TERRAIN_WIDTH
                && (p.y > height
                    || !terrain.Contains(new Point(p.x + inc, p.y))));

            if (canBePushed) {
                parts = parts.Select(p => new Point(p.x + inc, p.y)).ToHashSet();
            }
        }

        public HashSet<Point> Stop(HashSet<Point> terrain) {
            foreach (var p in parts) {
                terrain.Add(p);
            }
            return terrain;
        }
    }
}
