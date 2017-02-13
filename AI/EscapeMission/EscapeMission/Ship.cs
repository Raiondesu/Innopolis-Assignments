using System;
using System.Collections.Generic;
using System.Linq;

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
		private List<Matrix.Cell> UnknownNeighbours => this.Neighbours.Where(c => !c.Visited).ToList();

		public Matrix Map { get; }
		public Matrix.Cell Current => Path.Count == 0 ? null : Path.Last();
		public Matrix.Cell Previous => Path.Count < 2 ? null : Path[Path.Count - 2];
		public Matrix.Cell Destination { get; private set; }
		public List<Matrix.Cell> Path { get; private set; } = new List<Matrix.Cell>();

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

		private int MoveTo(Matrix.Cell cell)
		{
			//TODO pathfinding
			this.Path.Add(cell);
			this.Yarn.Push(Destination);

			cell.Visited = true;
			if (cell.IsEmpty) return 0;
			if (cell.HasPlanet) return 1;

			this.Deaths++;
			this.Path.RemoveAt(Path.Count - 1);
			this.Yarn.Pop();

			return -1;
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
			if (this.Yarn.Count >= 2)
			{
				if ((!Current.HasRadiation && UnknownNeighbours.Count == 0) || (Current.HasRadiation && !Current.HasPlanetRadiation))
				{
					this.Yarn.Pop();
					Destination = this.Yarn.Pop();
					return this.MoveTo(Destination);
				}
			}
//			else if (this.Yarn.Count == 1 && Path.Count > 1)
//			{
//				List<Matrix.Cell> cells = Map.Cells.OfType<Matrix.Cell>().ToList().FindAll(c => c.Visited);
//				cells = cells.FindAll(c =>
//				{
//					List<Matrix.Cell> known = c.Neighbours.FindAll(n => !n.Visited && !n.IsEmpty);
//					return known.Count > 2;
//				});
//				this.MoveTo(cells[_rand.Next(cells.Count)]);
//			}
			return this.MoveRandom();
		}

		private int Risk(List<Matrix.Cell> toRisk)
		{
			Destination = toRisk[_rand.Next(toRisk.Count)];
			this.MoveTo(Destination);
			return this.MoveRandom();
		}

		public void Escape(Algorithms algorithm = Algorithms.Backtracker)
		{
			if (this.MoveTo(Destination) == 1) return;

			switch (algorithm)
			{
				case Algorithms.Random:
					if (Log) Console.WriteLine(this.Map.CellsToString(true, Current.Location));
					do if (Log)Console.WriteLine(this.Map.CellsToString(true, Current.Location));
					while (this.MoveRandom() < 1 && this.Deaths < 10000);
					break;
				case Algorithms.Backtracker:
					if (Log) Console.WriteLine(this.Map.CellsToString(true, Current.Location));
					do if (Log) Console.WriteLine(this.Map.CellsToString(true, Current.Location));
					while (this.MoveTrack() < 1);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
			}
		}

		public enum Algorithms
		{
			Random,
			Backtracker
		}
	}
}