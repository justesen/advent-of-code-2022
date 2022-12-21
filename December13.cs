using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode
{
    class December11 {
        static void Main(string[] args) {
            var input = Parse();

            var res1 = input
                    .Select((pair, i) => Compare(pair.Item1, pair.Item2) < 0 ? i + 1 : 0)
                    .Sum();
            Console.WriteLine(res1);

            var divider1 = new Packet(new List<Packet>() {new Packet(new List<Packet>() { new Packet (2) })});
            var divider2 = new Packet(new List<Packet>() {new Packet(new List<Packet>() { new Packet (6) })});
            var packets = Flatten(input).Append(divider1).Append(divider2).ToList();
            packets.Sort(Compare);
            Console.WriteLine((packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1));
        }

        static List<Packet> Flatten(List<(Packet, Packet)> pairs) {
            var packets = new List<Packet>();
            foreach (var pair in pairs) {
                packets.Add(pair.Item1);
                packets.Add(pair.Item2);
            }
            return packets;
        }

        static List<(Packet, Packet)> Parse() {
            var input = new List<(Packet, Packet)>();
            string line1 = "";
            string line2 = "";

            while ((line1 = Console.ReadLine()) != null) {
                line2 = Console.ReadLine();
                input.Add((ParsePacket(line1, ""), ParsePacket(line2, "")));
                Console.ReadLine();
            }
            return input;
        }

        static Packet ParsePacket(string line, string indent) {
            if (line.StartsWith("[")) {
                Packet p = new Packet();
                int i = 1;

                while (i < line.Length - 1) {
                    if (line[i] == ',') {
                        i++;
                    }
                    int j = line.Substring(i).StartsWith("[")
                            ? MatchingBracket(line, i) + 1
                            : line.IndexOfAny(",]".ToCharArray(), i + 1);
                    p.packets.Add(ParsePacket(line.Substring(i, j - i), indent + "  "));
                    i = j;
                }
                return p;
            }
            return new Packet(Int32.Parse(line));
        }

        static int MatchingBracket(string line, int i) {
            int open_brackets = 1;

            while (open_brackets > 0) {
                i++;

                if (line[i] == '[') {
                    open_brackets++;
                } else if (line[i] == ']') {
                    open_brackets--;
                }
            }
            return i;
        }

        static int Compare(Packet p, Packet q) {
            if (p.packets == null && q.packets == null) {
                return p.packet - q.packet;
            } else if (p.packets != null && q.packets != null) {
                foreach (var (a, b) in p.packets.Zip(q.packets, (a, b) => (a, b))) {
                    int res = Compare(a, b);
                    if (res != 0) {
                        return res;
                    }
                }
                return p.packets.Count - q.packets.Count;
            }
            if (p.packets == null) {
                p = new Packet(new List<Packet>() { p });
            } else {
                q = new Packet(new List<Packet>() { q });
            }
            return Compare(p, q);
        }
    }

    class Packet {
        public readonly List<Packet> packets = null;
        public readonly int packet = -1;


        public Packet() {
            this.packets = new List<Packet>();
        }

        public Packet(int packet) {
            this.packet = packet;
        }

        public Packet(List<Packet> packets) {
            this.packets = packets;
        }

        public override string ToString() {
            if (packets != null) {
                return $"[{String.Join(",", packets.Select(p => p.ToString()))}]";
            } else {
                return packet.ToString();
            }
        }
    }
}
