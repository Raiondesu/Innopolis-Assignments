using System;
using System.IO;
using System.Linq;

namespace EscapeMission
{
    internal class Program
    {
        public static void Main(string[] args)
			=> Escape(
				args.Contains("test") ?
					Matrix.Generate() : new Matrix("input.txt", 100, 100, 100),
				args.Contains("log")
			);

	    public static void Escape(Matrix matrix, bool log = false)
	    {
		    StreamWriter file = File.CreateText("output.txt");
		    Ship pod = new Ship(matrix, log);

		    if (pod.Escape(Ship.Algorithms.AStar))
		    {
			    Console.WriteLine(pod.Path.Count);
			    for (var i = 0; i < pod.Path.Count; i++)
			    {
				    var cell = pod.Path[i];
				    string result = i == pod.bombedCellIndex ? "M " : "";
				    result += $"{cell.Location.X} {cell.Location.Y} {cell.Location.Z}\n";

				    file.Write(result);
			    }
			    file.Write($"{pod.Watch.ElapsedMilliseconds}ms\n");
		    }
			else file.Write("No way there is!\n");
	    }
    }
}