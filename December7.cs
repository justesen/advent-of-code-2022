using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December7 {
        static void Main(string[] args) {
            var root = Parse();
            root.Size();
            root.Print();

            var res1 = root.Find(f => f.size < 100000 && f.parent != null).Sum(f => f.size);
            Console.WriteLine(res1);

            int sizeToRemove = 30000000 - (70000000 - root.size);
            var res2 = root.Find(f => f.size >= sizeToRemove && f.parent != null).Min(f => f.size);
            Console.WriteLine(res2);
        }

        static FileTree Parse() {
            var root = new FileTree("/", null, 0);
            var f = root;
            string line = Console.ReadLine(); // Skip `cd /`
            bool skipRead = false;

            while (skipRead || (line = Console.ReadLine()) != null) {
                var tokens = line.Split();
                skipRead = false;

                if (line.StartsWith("$ ls")) {
                    skipRead = true;
                    while ((line = Console.ReadLine()) != null) {
                        if (line.StartsWith("$")) {
                            break;
                        }
                        tokens = line.Split();

                        if (tokens[0] == "dir") {
                            f.contents.Add(new FileTree(tokens[1], f, 0));
                        } else {
                            f.contents.Add(new FileTree(tokens[1], null, Int32.Parse(tokens[0])));
                        }
                    }
                    if (line == null) {
                        break;
                    }
                } else if (line.StartsWith("$ cd ..")) {
                    f = f.parent;
                } else if (line.StartsWith("$ cd")) {
                    f = f.contents.Find(c => c.name == tokens[2]);
                } else {
                    Console.WriteLine("Unknown command: " + line);
                }
            }
            return root;
        }
    }

    class FileTree {
        public readonly string name;
        public readonly FileTree parent;
        public int size;
        public readonly List<FileTree> contents;

        public FileTree(string name, FileTree parent, int size) {
            this.name = name;
            this.parent = parent;
            this.size = size;
            this.contents = new List<FileTree>();
        }

        public void Print() {
            Print("");
        }

        public void Print(string indent) {
            Console.WriteLine($"{indent}{name} {size}");
            contents.ForEach(c => c.Print(indent + "  "));
        }

        public int Size() {
            if (contents.Count > 0) {
                size = contents.Select(c => c.Size()).Sum();
            }
            return size;
        }

        public List<FileTree> Find(Func<FileTree, bool> pred) {
            var res = new List<FileTree>();

            if (pred(this)) {
                res.Add(this);
            }
            contents.ForEach(c => res = res.Concat(c.Find(pred)).ToList());

            return res;
        }
    }
}
