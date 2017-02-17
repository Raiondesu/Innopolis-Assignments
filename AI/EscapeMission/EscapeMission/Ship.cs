using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EscapeMission
{
	public class Ship
	{
		private const int DeathLimit = 100;
		private readonly Random _rand = new Random();
		private bool _hasBomb = true;
		private bool Log { get; }


		public Ship(Matrix matrix, bool log = false)
		{
			this.Map = new Matrix(matrix);
			this.Destination = this.Map.Root;
			this.Log = log;
		}

		public Matrix Map { get; }

		public Matrix.Cell Current => Path.Count == 0 ? null : Path.Last();
		public Matrix.Cell Previous => Path.Count < 2 ? null : Path[Path.Count - 2];
		public Matrix.Cell Destination { get; private set; }

		public List<Matrix.Cell> Path { get; } = new List<Matrix.Cell>();
		public List<Matrix.Cell> Visited { get; } = new List<Matrix.Cell>();
		public List<Matrix.Cell> UnknownNeighbours => this.Neighbours.FindAll(c => !c.Visited);
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

		public Stopwatch Watch { get; } = new Stopwatch();

		private int SettleIn(Matrix.Cell cell, bool restart = false)
		{
			this.Visited.Add(cell);
			this.Path.Add(cell);
			this.Yarn.Push(cell);

			cell.Visited = true;
			if (cell.IsEmpty) return 0;
			if (cell.HasPlanet) return 1;

			this.Deaths++;

			if (restart)
			{
				this.Path.Clear();
				this.Yarn.Clear();
			}
			else
			{
				this.Path.RemoveAt(this.Path.Count - 1);
				this.Yarn.Pop();
			}

			return 0;
		}

		private int MoveToDestination()
		{
			if (Current == null)
				this.SettleIn(this.Map.Root);

			if (Destination.Location == Current.Location)
				return 0;

			var path = ASStar.FindPath(Current, Destination, Map.Cells);

			if (path == null)
			{
				this.Deaths++;
				return -1;
			}

			this.Path.AddRange(path);

			return this.SettleIn(Destination);
		}

		private int MoveRandom(bool restart = false)
		{
			List<Matrix.Cell> near = Neighbours,
			                  unknown = near.FindAll(c => !c.Visited);

			if (unknown.Count > 0)
				do Destination = unknown[_rand.Next(unknown.Count)];
				while (Destination.Visited);
			else
				do Destination = near[_rand.Next(near.Count)];
				while (!Destination.IsEmpty);

			if (this.Deaths >= DeathLimit - 1) // If we approach death...
			{
				if (Destination.IsEmpty || !Destination.HasRadiation)
				{
					this.SettleIn(Destination);
					near = Neighbours;
					Destination = near[_rand.Next(near.Count)];
				}
				if (Destination.HasKrakenRadiation)
					this.FireAt(Destination);
			}

			return this.SettleIn(Destination, restart);
		}

		private int MoveTrack()
		{
			if (Previous?.HasPlanetRadiation ?? false) //Prevoius? Has planet radiation?? How could I miss THAT??? Go there!
				return this.SettleIn(Previous);

			if (UnknownNeighbours.Count != 0)
				return this.MoveRandom();

			if (this.Yarn.Count >= 2)
			{
				this.Yarn.Pop();
				return this.SettleIn(this.Yarn.Pop());
			}

			return Risk();
		}

		private int Risk()
		{
			var krakensR = this.Visited.FindAll(c => c.HasKrakenRadiation && c.IsEmpty);
			List<Matrix.Cell> krakens = null;
			for (var i = 0; i < krakensR.Count && krakens == null; i++)
			{
				Destination = krakensR[i];

				this.MoveToDestination();

				krakens = this.Neighbours.FindAll(c => c.HasKraken);

				if (krakens.Count == 0) krakens = null;
				else break;
			}

			var kraken = krakens?[0];

			if (this.FireAt(kraken))
			{
				this.SettleIn(kraken);
				return this.MoveTrack();
			}

			return -1;
		}

		private bool FireAt(Matrix.Cell danger)
		{
			if (this._hasBomb && danger != null && !danger.HasBlackHole)
			{
				danger.Occupant = Matrix.OccupantType.Empty;
				this._hasBomb = false;
				return true;
			}
			return false;
		}

		public bool Escape(Algorithms algorithm = Algorithms.Random)
		{
			int code;
			Func<int> solver = () => this.MoveRandom(true);
			Func<bool> isAlive = () => this.Deaths < 100;

			if (this.SettleIn(this.Map.Root) == 1) return true;

			if (algorithm == Algorithms.Backtracker)
			{
				isAlive = () => true;
				solver = this.MoveTrack;
			}
			else if (algorithm == Algorithms.AStar)
			{
				isAlive = () => this.Deaths < 1;
				this.Map.Cells.OfType<Matrix.Cell>().All(n => n.Visited = true);
				this.Destination = this.Map.Cells.OfType<Matrix.Cell>().First(n => n.HasPlanet);
				solver = this.MoveToDestination;
			}

			Watch.Start();
			do code = solver();
			while (isAlive() && code == 0);
			Watch.Stop();

			if (Log) Console.WriteLine(this.Map.CellsToString(false, this.Path));
			return code == 1;
		}

		public enum Algorithms
		{
			Random,
			Backtracker,
			AStar
		}
	}
}