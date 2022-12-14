using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December11 {
        static void Main(string[] args) {
            var monkeys1 = Parse();
            var monkeys2 = monkeys1.Select(m => new Monkey(m)).ToArray();

            ThrowStuff(monkeys1, 20, w => w / 3);
            var res1 = monkeys1
                    .Select(m => m.inspections)
                    .OrderByDescending(x => x)
                    .Take(2)
                    .Aggregate((acc, x) => acc*x);
            Console.WriteLine(res1);

            int mod = monkeys2.Select(m => m.test).Aggregate((a, b) => a * b);
            ThrowStuff(monkeys2, 10000, w => w % mod);
            var res2 = monkeys2
                    .Select(m => m.inspections)
                    .OrderByDescending(x => x)
                    .Take(2)
                    .Aggregate((acc, x) => acc*x);
            Console.WriteLine(res2);
        }

        static Monkey[] Parse() {
            var input = new List<Monkey>();
            string line = "";
            int i = 0;
            var operations = new Func<long, long>[] {
                x => x * 7,
                x => x * 17,
                x => x + 2,
                x => x + 1,
                x => x + 6,
                x => x * x,
                x => x + 3,
                x => x + 4,
            };

            while ((line = Console.ReadLine()) != null) {
                Monkey m = new Monkey();

                line = Console.ReadLine();
                line.Substring(18).Split(", ").ToList().ForEach(x => m.items.Enqueue(Int64.Parse(x)));

                Console.ReadLine();
                m.operation = operations[i];
                i++;

                m.test = Int32.Parse(Console.ReadLine().Split().Last());;

                m.monkeyTrue = Int32.Parse(Console.ReadLine().Split().Last());

                m.monkeyFalse = Int32.Parse(Console.ReadLine().Split().Last());

                Console.ReadLine();
                input.Add(m);
            }
            return input.ToArray();
        }

        static void ThrowStuff(Monkey[] monkeys, int rounds, Func<long, long> manage_worry) {
            for (int i = 1; i < rounds + 1; i++) {
                foreach (var m in monkeys) {
                    while (m.items.Count > 0) {
                        var item = m.items.Dequeue();
                        var worry = manage_worry(m.operation(item));
                        m.inspections++;

                        if (worry % m.test == 0) {
                            monkeys[m.monkeyTrue].items.Enqueue(worry);
                        } else {
                            monkeys[m.monkeyFalse].items.Enqueue(worry);
                        }
                    }
                }
            }
        }
    }

    class Monkey {
        public Queue<long> items;
        public Func<long, long> operation;
        public int test;
        public int monkeyTrue;
        public int monkeyFalse;
        public long inspections;

        public Monkey() {
            this.items = new Queue<long>();
            this.operation = x => x;
            this.test = 1;
            this.monkeyTrue = 0;
            this.monkeyFalse = 0;
            this.inspections = 0;
        }

        public Monkey(Monkey m) {
            this.items = new Queue<long>(m.items);
            this.operation = m.operation;
            this.test = m.test;
            this.monkeyTrue = m.monkeyTrue;
            this.monkeyFalse = m.monkeyFalse;
            this.inspections = m.inspections;
        }
    }
}
