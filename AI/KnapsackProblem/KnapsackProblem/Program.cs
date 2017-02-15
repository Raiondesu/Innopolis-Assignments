using System;
using System.IO;

namespace KnapsackProblem
{
	internal class Program
	{
		public static void Main(string[] args)
		{
//			string[] lines = File.ReadAllLines("input.txt");
			string[] lines = "2\n5\n50\n10 120 100 10 60\n5 30 5 20 1\n6\n60\n10 120 100 10 60 5\n5 30 5 20 10 1".Split('\n');

			uint tests = uint.Parse(lines[0]);

			int length, capacity;
			int[] values, weights;
			string[] line;

			for (int i = 1; i <= tests*4; i += 4)
			{
				length = int.Parse(lines[i]);
				capacity = int.Parse(lines[i + 1]);

				values = new int[length];
				weights = new int[length];

				line = lines[i + 2].Split(' ');
				for (var j = 0; j < line.Length; j++)
					values[j] = int.Parse(line[j]);

				line = lines[i + 3].Split(' ');
				for (var j = 0; j < line.Length; j++)
					weights[j] = int.Parse(line[j]);

				Console.WriteLine(Knapsack(values, weights, capacity, values.Length));
			}
		}

		public static int Knapsack(int[] vals, int[] weights, int capacity, int n)
		{
			if (n == 0 || capacity == 0) return 0;

			int a = Knapsack(vals, weights, capacity, n - 1);

			if (weights[n - 1] > capacity) return a;

			int b = vals[n - 1] + Knapsack(vals, weights, capacity - weights[n - 1], n - 1);
			return a > b ? a : b;
		}
	}
}