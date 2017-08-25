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

			

			ConstrainedMultiset<string> c = new ConstrainedMultiset<string>(3);
			c.Add("Yay", 3);
			c.Add("Yay");
			c.Add("Lol", 2);
			c.Add("Dota", 5);
			Console.WriteLine("ConstrainedMultiset \"c\": " + c);
			Console.WriteLine("c[\"Dota\"]: " + c["Dota"]);
			Console.WriteLine();

			

			ConstrainedMultiset<Multiset<string>> multiset = new ConstrainedMultiset<Multiset<string>>(10);
			multiset.Add(m, 2);
			multiset.Add(n);
			multiset.Add(c, 11);
			Console.WriteLine("ConstrainedMultiset of multisets of strings: " + multiset);
			Console.WriteLine();
		}
	}
}
