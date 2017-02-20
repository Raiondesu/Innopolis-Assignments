using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EscapeMission
{
	public class Matrix
	{
		public Matrix(Vector size) : this(size.X, size.Y, size.Z){}

		public Matrix(int x, int y, int z)
		{
			Size = new Vector(x, y, z);
			Cells = new Cell[x, y, z];
			for (var j = 0; j < x; j++)
			for (var k = 0; k < y; k++)
			for (var l = 0; l < z; l++)
				Cells[j, k, l] = new Cell(j, k, l, this) {Visited = true};
		}

		public Matrix(string filename, Vector size) : this(filename, size.X, size.Y, size.Z){}

		public Matrix(string filename, int x, int y, int z) : this(x, y, z)
		{
			string[] lines = File.ReadAllLines(filename);
			foreach (string t in lines)
			{
				var line = t.Split(' ');
				var coord = new Vector(int.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3]));
				if (line[0] == "P")
					Cells[coord.X, coord.Y, coord.Z].Occupant |= OccupantType.Planet;
				else if (line[0] == "K")
					Cells[coord.X, coord.Y, coord.Z].Occupant |= OccupantType.Kraken;
				else if (line[0] == "B")
					Cells[coord.X, coord.Y, coord.Z].Occupant |= OccupantType.Blackhole;
			}
		}

		public Matrix(Matrix matrix)
		{
			Size = matrix.Size;
			Cells = new Cell[Size.X, Size.Y, Size.Z];
			for (var j = 0; j < Size.X; j++)
			for (var k = 0; k < Size.Y; k++)
			for (var l = 0; l < Size.Z; l++)
				Cells[j, k, l] = new Cell(matrix.Cells[j, k, l]) {Visited = false};
		}

		public Cell[,,] Cells { get; }
		public Cell Root { get { return Cells[0, 0, 0]; } set { Cells[0, 0, 0] = value; } }

		public Vector Size { get; }

		public class Cell
		{
			private Matrix Parent { get; }
			private OccupantType _occupant;

			public Cell(int x, int y, int z, Matrix parent)
			{
				Location = new Vector(x, y, z);
				Parent = parent;
				Occupant = OccupantType.Empty;
			}

			public Cell(Vector coordinate, Matrix parent)
				: this(coordinate.X, coordinate.Y, coordinate.Z, parent) {}

			public Cell(Cell cell)
			{
				this.Location = cell.Location;
				this.Parent = cell.Parent;
				this.Occupant = cell.Occupant;
			}

			public Vector Location { get; }
			public int X => Location.X;
			public int Y => Location.Y;
			public int Z => Location.Z;

			public OccupantType Occupant
			{
				get { return Visited ? _occupant : OccupantType.Unknown; }
				set { _occupant = value; }
			}

			public bool HasKraken => Occupant.HasFlag(OccupantType.Kraken);
			public bool HasBlackHole => Occupant.HasFlag(OccupantType.Blackhole);
			public bool HasPlanet => Occupant.HasFlag(OccupantType.Planet);
			public bool IsEmpty => Occupant == OccupantType.Empty;
			public bool IsUnknown => Occupant == OccupantType.Unknown;

			public OccupantType Radiation
				=> Visited ?
					Neighbours.Aggregate(Occupant, (current, cell) => current | cell.Occupant) :
					OccupantType.Unknown;

			public bool HasKrakenRadiation => Radiation.HasFlag(OccupantType.Kraken);
			public bool HasBlackHoleRadiation => Radiation.HasFlag(OccupantType.Blackhole);
			public bool HasPlanetRadiation => Radiation.HasFlag(OccupantType.Planet);
			public bool HasRadiation => Radiation != OccupantType.Empty;
			public bool RadiationIsUnknown => Radiation == OccupantType.Unknown;

			public bool Visited { get; set; }

			public int NeigboursAmount => Neighbours.Count;
			private List<Cell> Neighbours
			{
				get
				{
					List<Cell> near = new List<Cell>();
					if (X + 1 < Parent.Size.X)
						near.Add(Parent.Cells[X + 1, Y, Z]);
					if (X - 1 >= 0)
						near.Add(Parent.Cells[X - 1, Y, Z]);
					if (Y + 1 < Parent.Size.Y)
						near.Add(Parent.Cells[X, Y + 1, Z]);
					if (Y - 1 >= 0)
						near.Add(Parent.Cells[X, Y - 1, Z]);
					if (Z + 1 < Parent.Size.Z)
						near.Add(Parent.Cells[X, Y, Z + 1]);
					if (Z - 1 >= 0)
						near.Add(Parent.Cells[X, Y, Z - 1]);
					return near;
				}
			}

			public string CellToString(bool printRad = true)
			{
				if (printRad && this.IsEmpty)
					return (!this.HasRadiation ? "·" : ((int) this.Radiation).ToString());
				if (this.IsUnknown)
					return "■";
				if (this.HasPlanet)
					return "P";
				if (this.HasBlackHole)
					return "B";
				if (this.HasKraken)
					return "K";
				return "·";
			}

			public override string ToString() => $"[{X}, {Y}, {Z}] = {this.CellToString()}";

			public static implicit operator int(Cell cell) => (int) cell.Occupant;
		}

		[Flags]
		public enum OccupantType
		{
			Unknown = -1,
			Empty = 0,
			Blackhole = 1,
			Kraken = 2,
			Planet = 4
		}

		public override string ToString()
			=> $"\nRadiation:\n{this.CellsToString()}\nMap:\n{CellsToString(false)}\n\n";

		public string CellsToString(bool printRad, Vector special)
			=> CellsToString(printRad, special == null ? null : new List<Cell> {this.Cells[special.X, special.Y, special.Z]});

		public string CellsToString(bool printRad = true, Cell special = null)
			=> CellsToString(printRad, special == null ? null : new List<Cell> {special});

		public string CellsToString(bool printRad, List<Cell> special)
		{
			string result = "";
			for (var z = 0; z < Size.Z; z++)
			{
				for (var y = -1; y <= Size.Y; y++)
				{
					result += "\n";
					for (var x = -1; x <= Size.X; x++)
					{
						if (y == -1)
							result += x == -1 ? "┌" : x == Size.X ? "┐" : "───";
						else if (y == Size.Y)
							result += x == -1 ? "└" : x == Size.X ? "┘" : "───";
						else if (x == -1 || x == Size.X)
							result += "│";
						else if (special == null || special.Count == 0 || special.All(v =>  v.X != x || v.Y != y || v.Z != z))
							result += $" {Cells[x, y, z].CellToString(printRad)} ";
						else if (special.Any(v => v.X == x && v.Y == y && v.Z == z))
							result += $"[{Cells[x, y, z].CellToString(printRad)}]";
					}
				}
				result += "\n";
			}
			return result;
		}

		public static Matrix Generate()
		{
			StreamWriter file = File.CreateText("input.txt");
			Random rand = new Random();
			List<Vector> notUsed = new List<Vector>();

			string line = Console.ReadLine();
			string[] args = line?.Length > 0 ? line.Split(' ') : new[] {"60", "30", "1", "10", "10"};

			Vector size = new Vector(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
			int percentageB = int.Parse(args[3]);
			int percentageK = int.Parse(args[4]);
			int percentage = percentageB + percentageK;

			for (int x = 0; x < size.X; x++)
			for (int y = 0; y < size.Y; y++)
			for (int z = 0; z < size.Z; z++)
			{
				int d = rand.Next(0, 100);
				bool obstacle = d < percentage;
				if (!obstacle)
					notUsed.Add(new Vector(x, y, z));
				else if (x > 0 || y > 0 || z > 0)
					file.Write((rand.Next(percentage) < percentageK ? "K " : "B ") + new Vector(x, y, z) + "\n");
			}

			file.Write("P " + notUsed[rand.Next(notUsed.Count)] + "\n");
			file.Flush();
			file.Close();

			return new Matrix("input.txt", size);
		}
	}
}