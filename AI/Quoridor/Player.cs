using System;

namespace Quoridor
{
	using Algorithms;

	public class Player : IFormattable
	{
		public Player(string name, Type algorithm, ref QuoridorGame.Board board, int wallsAmount = Defaults.WallsAmount)
		{
			this.Name = name ?? "Nobody";
			this.WallsAmount = wallsAmount;
			this.Board = board;
			this.PlayAlgorithm = algorithm.Construct(this) as Algorithm;
		}

		public string Name { get; private set; }
		public int WallsAmount { get; private set; }
		public int Steps { get; private set; } = 0;
		public QuoridorGame.Board Board { get; }
		public Algorithm PlayAlgorithm { get; }
		public Vector2D Location { get; private set; } = new Vector2D();
		public bool HasWon => System.Math.Abs(this.Location.Y - Board.Size) == Board.Size - this.Location.Y;

		public void SetInitial(Vector2D initLocation)
			=> this.Location = this.Steps == 0 ? initLocation : this.Location;

		public void Step(Vector2D mUnitVector)
			=> this.Move(mUnitVector.Clamp(-Vector2D.Unit, Vector2D.Unit) * 2);

		public bool Move(Vector2D mVector)
		{
			var loc = this.Location + mVector;
			if (loc > 0 && loc < Board.Size2D && !Board.HasObstacleBetween(this, loc))
			{
				this.Location = loc;
				this.Steps++;
			} else Console.WriteLine("error");

			return this.HasWon;
		}

		public bool PlaceWall(Wall wall)
		{
			if (this.Board.TryAddWall(this, wall))
			{
				this.WallsAmount--;
				this.Steps++;
				return true;
			} return false;
		}

		public string ToString(string format, IFormatProvider formatProvider)
			=> $"{this.Name} ({(format == "G" ? this.PlayAlgorithm?.Name : this.PlayAlgorithm?.ShortName)})";


		///Directions
		public static Vector2D Up => new Vector2D(0, 1);
		public static Vector2D Down => new Vector2D(0, -1);
		public static Vector2D Right => new Vector2D(1, 0);
		public static Vector2D Left => new Vector2D(-1, 0);

		public static Player Nobody
		{
			get
			{
				var b = QuoridorGame.Board.Empty;
				return new Player(null, null, ref b);
			}
		}
	}
}