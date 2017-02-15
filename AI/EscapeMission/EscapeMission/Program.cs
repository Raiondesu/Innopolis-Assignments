using System;
using System.Linq;

namespace EscapeMission
{
    internal class Program
    {
        public static void Main(string[] args)
        {
	        if (args.Contains("test"))
		        Escape(Matrix.Generate(), args.Contains("log"));
	        else
		        Escape(new Matrix("input.txt", new Vector(60, 30, 1)), true);
        }

	    public static void Escape(Matrix matrix, bool log = false)
	    {
//		    if (matrix.Size.Z == 1 && log) Console.WriteLine(matrix);

		    Ship pod = new Ship(matrix, log);

		    pod.Escape(Ship.Algorithms.AStar);

//		    if (matrix.Size.Z == 1 && log) Console.WriteLine(pod.Map);

		    Console.WriteLine(pod.Deaths);
		    Console.WriteLine(pod.Path.Count);
//		    foreach (var cell in pod.Path)
//			    Console.WriteLine($"{cell.Location.X} {cell.Location.Y} {cell.Location.Z}");
		    Console.WriteLine($"{pod.Watch.ElapsedMilliseconds}ms");
	    }
    }
}