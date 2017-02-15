using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace EscapeMission
{
	public class Ship
	{
		public Ship(Matrix matrix, bool log = false)
		{
			this.Map = new Matrix(matrix);
			this.Destination = this.Map.Root;
			this.Log = log;
		}

		//		private bool _hasBomb = true;
		private bool Log { get; }

		private readonly Random _rand = new Random();
		private List<Matrix.Cell> UnknownNeighbours => this.Neighbours.FindAll(c => !c.Visited);

		public Matrix Map { get; }
		public Matrix.Cell Current => Path.Count == 0 ? null : Path.Last();
		public Matrix.Cell Previous => Path.Count < 2 ? null : Path[Path.Count - 2];
		public Matrix.Cell Destination { get; private set; }
		public List<Matrix.Cell> Path { get; private set; } = new List<Matrix.Cell>();

		public Stopwatch Watch { get; } = new Stopwatch();

		public List<Matrix.Cell> Neighbours
		{
			get
			{
				List<Matrix.Cell> near = new List<Matrix.Cell>();
				if (X + 1 < Map.Size.X)
					near.Add(Map.Cells[X + 1, Y, Z]);
				if (X - 1 >= 0)
					near.Add(Map.Cells[X - 1, Y, Z]);
				if (Y + 1 < Map.Size.Y)
					near.Add(Map.Cells[X, Y + 1, Z]);
				if (Y - 1 >= 0)
					near.Add(Map.Cells[X, Y - 1, Z]);
				if (Z + 1 < Map.Size.Z)
					near.Add(Map.Cells[X, Y, Z + 1]);
				if (Z - 1 >= 0)
					near.Add(Map.Cells[X, Y, Z - 1]);
				return near;
			}
		}

		public Stack<Matrix.Cell> Yarn { get; } = new Stack<Matrix.Cell>();
		public uint Deaths { get; private set; }

		public int X => Current?.X ?? 0;
		public int Y => Current?.Y ?? 0;
		public int Z => Current?.Z ?? 0;

		private int SettleIn(Matrix.Cell cell)
		{
			this.Path.Add(cell);
			this.Yarn.Push(cell);

			cell.Visited = true;
			if (cell.IsEmpty) return 0;
			if (cell.HasPlanet) return 1;

			this.Deaths++;
			this.Path.RemoveAt(Path.Count - 1);
			this.Yarn.Pop();

			return -1;
		}

		private int MoveTo(Matrix.Cell cell)
		{
			if (Current == null || ASStar.EstimatePathLengthSqr(Current.Location, cell.Location) == 1)
				return this.SettleIn(cell);

			var path = ASStar.FindPath(Current, cell, Map.Cells);

			if (path == null) return 1;

			//...
			//			for (var i = 0; i < path.Count; i++)
			//			{
			//				if (Log)
			//				{
			//					Console.WriteLine(this.Map.CellsToString(false, path.GetRange(0, i)));
			//					Thread.Sleep(200);
			//				}
			//				this.SettleIn(this.Map.Cells[path[i].X, path[i].Y, path[i].Z]);
			//			}
			//			if (Log)
			//			{
			//				Console.WriteLine(this.Map.CellsToString(false, path));
			//				Thread.Sleep(200);
			//			}

			this.Path.AddRange(path);

			if (Log)
			{
				for (var i = 0; i < this.Path.Count; i++)
				{
					Console.WriteLine(this.Map.CellsToString(false, this.Path.GetRange(0, i)));
					Thread.Sleep(200);
				}
				Console.WriteLine(this.Map.CellsToString(false, this.Path));
				Thread.Sleep(200);
			}

			return this.SettleIn(cell);
		}

		private int MoveRandom(bool wise = true)
		{
			List<Matrix.Cell> near = UnknownNeighbours;
			if (wise && near.Count > 0)
				do Destination = near[_rand.Next(near.Count)];
				while (Destination.Visited);
			else
				do Destination = Neighbours[_rand.Next(Neighbours.Count)];
				while (!Destination.IsEmpty);
			return this.MoveTo(Destination);
		}

		private int MoveTrack()
		{
			if (Previous?.HasPlanetRadiation ?? false) //Prevoius? Has planet radiation?? How could I miss THAT??? Go there!
				return this.MoveTo(Previous);
			if (this.Yarn.Count >= 2 && ((Current.HasBlackHoleRadiation && Current.HasKrakenRadiation)
			                             || UnknownNeighbours.Count == 0))
			{
				this.Yarn.Pop();
				Destination = this.Yarn.Pop();
				return this.MoveTo(Destination);
			}
			return this.MoveRandom();
		}

		public void Escape(Algorithms algorithm = Algorithms.Backtracker)
		{
			if (this.MoveTo(Destination) == 1) return;

			switch (algorithm)
			{
				case Algorithms.Random:
					Watch.Start();
					do if (Log) Console.WriteLine(this.Map.CellsToString(true, Current));
					while (this.MoveRandom() < 1);
					Watch.Stop();
					break;
				case Algorithms.Backtracker:
					Watch.Start();
					do if (Log) Console.WriteLine(this.Map.CellsToString(true, Current));
					while (this.MoveTrack() < 1);
					Watch.Stop();
					break;
				case Algorithms.AStar:
					this.Destination = this.Map.Cells.OfType<Matrix.Cell>().First(n => n.HasPlanet);
					Watch.Start();
					this.MoveTo(Destination);
					Watch.Stop();
					if (Log) Console.WriteLine(this.Map.CellsToString(false, this.Path));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
			}
		}

		public enum Algorithms
		{
			Random,
			Backtracker,
			AStar
		}
	}
}