using System;

namespace MultisetTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Multiset<string> m = new Multiset<string>();
            m.Add("Yay", 3);
            m.Add("Yay");
            m.Add("Lol", 3);
            m.Add("Dota", 5);
            Console.WriteLine("Multiset \"m\": " + m);
            Console.WriteLine("m[\"Lol\"]: " + m["Lol"]);
            Console.WriteLine();

            Multiset<string> n = new Multiset<string>();
            n.Add("Yay", 4);
            n.Add("Lol");
            Console.WriteLine("Multiset \"n\": " + n);
            Console.WriteLine("n == m is " + (n == m).ToString().ToLower());
            Console.WriteLine();

            Console.WriteLine("`m - n` or `m.Substract(n)`: " + (m - n));

            m.Remove("Lol", 2);
            Console.WriteLine("Multiset \"m\" after `m.Remove(\"Lol\", 2)`: " + m);
            m.Remove("Dota");
            Console.WriteLine("Multiset \"m\" after `m.Remove(\"Dota\")`: " + m);
            Console.WriteLine();
            Console.WriteLine("n == m is " + (n == m).ToString().ToLower());
            Console.WriteLine();

            Multiset<Multiset<string>> multiset = new Multiset<Multiset<string>>();
            multiset.Add(m, 2);
            multiset.Add(n);
            
            Console.WriteLine("Multiset of multisets of strings: " + multiset);
        }
    }
}
