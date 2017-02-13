using System;

namespace EscapeMission
{
    internal class Program
    {
        public static void Main(string[] args)
			=> Escape(new Matrix("input.txt", new Vector(50, 50, 1)));

	    public static void Escape(Matrix matrix)
	    {
//		    if (matrix.Size.Z == 1) Console.WriteLine(matrix);

		    Ship pod = new Ship(matrix, false);

		    var watch = System.Diagnostics.Stopwatch.StartNew();
		    pod.Escape(Ship.Algorithms.Backtracker);
		    watch.Stop();

//		    if (matrix.Size.Z == 1) Console.WriteLine(pod.Map);

//		    foreach (var cell in pod.Path)
//			    Console.WriteLine($"{cell.Location.X} {cell.Location.Y} {cell.Location.Z}");
		    Console.WriteLine($"{watch.ElapsedMilliseconds}ms");
		    //TODO move these guys up:
		    Console.WriteLine(pod.Path.Count);
		    Console.WriteLine(pod.Deaths);
	    }
    }
}