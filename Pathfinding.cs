using System;
using System.Linq;
using System.Collections.Generic;


namespace Pathfinding
{

    class Point : IEquatable<Point> {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override int GetHashCode() {
            return HashCode.Combine(x, y);
        }

        public override bool Equals(object obj) {
            return obj is Point p && Equals(p);
        }

        public bool Equals(Point p) {
            return x == p.x && y == p.y;
        }

        public override string ToString() {
            return $"({x}, {y})";
        }
    }

    public class SearchNode<T> {
        public readonly T node;
        public readonly SearchNode<T> parent;

        public SearchNode(T node, SearchNode<T> parent) {
            this.node = node;
            this.parent = parent;
        }
    }

    public class BFS<T> where T : IEquatable<T> {
        public static LinkedList<T> Search(T start, T goal, Func<T, IEnumerable<T>> neighbors) {
            var q = new Queue<SearchNode<T>>();
            var explored = new HashSet<T>();

            explored.Add(start);
            q.Enqueue(new SearchNode<T>(start, null));

            while (q.Count > 0) {
                var v = q.Dequeue();

                if (v.node.Equals(goal)) {
                    return PathTo(v);
                }

                foreach (var w in neighbors(v.node)) {
                    if (!explored.Contains(w)) {
                        explored.Add(w);
                        q.Enqueue(new SearchNode<T>(w, v));
                    }
                }
            }

            return null;
        }

        private static LinkedList<T> PathTo(SearchNode<T> n) {
            var path = new LinkedList<T>();

            while (n != null) {
                path.AddFirst(n.node);
                n = n.parent;
            }

            return path;
        }
    }
}
