using System;
using System.Reflection;

namespace Quoridor
{
	using Algorithms;

	public class Player : IFormattable
	{
		public Player(string name, Type algorithm, int wallsAmount = Defaults.WallsAmount)
		{
			this.Name = name;
			this.WallsAmount = wallsAmount;
			this.PlayAlgorithm = (algorithm ?? Defaults.Algorithm)
				.GetConstructor(new [] { typeof(Player) })
				?.Invoke(new [] { this }) as Algorithm;
		}

		public string Name { get; private set; }
		public int WallsAmount { get; private set; }
		public Algorithm PlayAlgorithm { get; private set; }
		public Board GameBoard { get; private set; }
		public Vector2D Location { get; private set; }

		public void Step(Vector2D mUnitVector)
			=> this.Move(mUnitVector.Clamp(-Vector2D.Unit, Vector2D.Unit) * 2);

		public void Move(Vector2D mVector)
		{
			var loc = this.Location + mVector;
			if (loc >= 0 && loc <= GameBoard.Size2D)
				this.Location = loc;
		}

		public string ToString(string format, IFormatProvider formatProvider)
			=> $"{this.Name} ({(format == "G" ? this.PlayAlgorithm.Name : this.PlayAlgorithm.ShortName)})";
	}
}