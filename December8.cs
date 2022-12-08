using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December8 {
        static void Main(string[] args) {
            var grid = Parse();
            int n = grid.GetLength(0);

            var res1 = VisibleTrees(grid, n);
            Console.WriteLine(res1);

            var res2 = MostScenicTree(grid, n);
            Console.WriteLine(res2);
        }

        static int[,] Parse() {
            string line = Console.ReadLine();
            int n = line.Length;
            var input = new int[n, n];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    input[i, j] = line[j] - '0';
                }
                line = Console.ReadLine();
            }
            return input;
        }

        static int VisibleTrees(int[,] grid, int n) {
            var visible = new bool[n, n];
            int tallest_tree = -1;

            for (int i = 0; i < n; i++) {
                tallest_tree = -1;
                for (int j = 0; j < n; j++) {
                    visible[i, j] = visible[i, j] || grid[i, j] > tallest_tree;
                    tallest_tree = Math.Max(grid[i, j], tallest_tree);
                }

                tallest_tree = -1;
                for (int j = n - 1; j >= 0; j--) {
                    visible[i, j] = visible[i, j] || grid[i, j] > tallest_tree;
                    tallest_tree = Math.Max(grid[i, j], tallest_tree);
                }
            }

            for (int j = 0; j < n; j++) {
                tallest_tree = -1;
                for (int i = 0; i < n; i++) {
                    visible[i, j] = visible[i, j] || grid[i, j] > tallest_tree;
                    tallest_tree = Math.Max(grid[i, j], tallest_tree);
                }

                tallest_tree = -1;
                for (int i = n - 1; i >= 0; i--) {
                    visible[i, j] = visible[i, j] || grid[i, j] > tallest_tree;
                    tallest_tree = Math.Max(grid[i, j], tallest_tree);
                }
            }

            int visible_trees = 0;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (visible[i, j]) {
                        visible_trees++;
                    }
                }
            }

            return visible_trees;
        }

        static int MostScenicTree(int[,] grid, int n) {
            int best = 0;


            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    best = Math.Max(best, ScenicScore(grid, n, i, j));
                }
            }

            return best;
        }

        static int ScenicScore(int[,] grid, int n, int i, int j) {
            int height = grid[i, j];
            int score = 1;

            int visible_trees = 0;
            for (int y = i - 1; y >= 0; y--) {
                visible_trees++;
                if (grid[y, j] >= height) {
                    break;
                }
            }
            score *= visible_trees;

            visible_trees = 0;
            for (int y = i + 1; y < n; y++) {
                visible_trees++;
                if (grid[y, j] >= height) {
                    break;
                }
            }
            score *= visible_trees;

            visible_trees = 0;
            for (int x = j - 1; x >= 0; x--) {
                visible_trees++;
                if (grid[i, x] >= height) {
                    break;
                }
            }
            score *= visible_trees;

            visible_trees = 0;
            for (int x = j + 1; x < n; x++) {
                visible_trees++;
                if (grid[i, x] >= height) {
                    break;
                }
            }
            score *= visible_trees;

            return score;
        }
    }
}
