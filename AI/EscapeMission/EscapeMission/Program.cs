using System;
using System.Linq;

namespace EscapeMission
{
    internal class Program
    {
        public static void Main(string[] args)
			=> Escape(
				args.Contains("test") ?
					Matrix.Generate() : new Matrix("input.txt", 10, 10, 1),
				args.Contains("log")
			);

	    public static void Escape(Matrix matrix, bool log = false)
	    {
		    Ship pod = new Ship(matrix, log);

		    if (pod.Escape(Ship.Algorithms.Random))
		    {
			    Console.WriteLine(pod.Path.Count);
//				foreach (var cell in pod.Path)
//					Console.WriteLine($"{cell.Location.X} {cell.Location.Y} {cell.Location.Z}");
			    Console.WriteLine($"{pod.Watch.ElapsedMilliseconds}ms");
		    }
			else Console.WriteLine("No way there is!");
	    }
    }
}