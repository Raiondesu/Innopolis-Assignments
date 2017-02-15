using System;
using System.Collections.Generic;
using System.IO;

namespace MapGenerator
{
	internal class Vector
	{
		public Vector(int x, int y, int z, string letter)
		{
			X = x;
			Y = y;
			Z = z;
			Letter = letter;
		}

		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }
		public string Letter { get; set; }

		public override string ToString() => $"{Letter} {X} {Y} {Z}";
	}

	internal class Program
	{
		public static void Main(string[] args)
		{
			StreamWriter writer = new StreamWriter("input.txt");
			Random rand = new Random();
			List<Vector> notUsed = new List<Vector>();

			args = args ?? Console.ReadLine()?.Split(' ') ?? new[] {"30", "10", "10", "true"};

			int size = int.Parse(args[0]);
			int percentageB = int.Parse(args[1]);
			int percentageK = int.Parse(args[2]);
			int percentage = percentageB + percentageK;
			bool is2D = bool.Parse(args.Length == 3 ? "false" : args[3]);

			for(int x = 0; x < size; x++)
			for(int y = 0; y < size; y++)
			for(int z = 0; z < (!is2D ? size : 1); z++)
			{
				bool obstacle = rand.Next(0, 100) < percentage;
				if (!obstacle)
					notUsed.Add(new Vector(x, y, z, "P"));
				else if (x > 0 || y > 0 || z > 0)
					writer.Write(new Vector(x, y, z, rand.Next(percentage) < percentageK ? "K" : "B") + "\n");
			}

			writer.Write(notUsed[rand.Next(notUsed.Count)] + "\n");
			writer.Flush();
		}
	}
}