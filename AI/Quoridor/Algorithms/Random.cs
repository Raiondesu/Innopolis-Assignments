using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Algorithms
{
	public class Random : Player
	{
		private System.Random rand = new System.Random();

		public Random(string name, int depth = 2, int wallsAmount = 10)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + " (Random)";

		public override bool Turn(int board, ref List<Wall> walls, Player opponent)
		{
			int move = rand.Next(2);
			return move > 0 ? this.TryMove(board, walls, opponent) : this.TryPlaceWall(board, ref walls);
		}

		private bool TryMove(int board, List<Wall> walls, Player opponent)
		{
			var directions = new [] {
				Directions.Up,
				Directions.Down,
				Directions.Left,
				Directions.Right,
			};

			var i = rand.Next() % 4;

			for (int k = 0; k < 4; k++)
				if (base.TryMove(directions[(i + k) % 4], board, walls, opponent))
					return true;

			return false;
		}

		private bool TryPlaceWall(int board, ref List<Wall> walls)
		{
			if (this.WallsAmount == 0) return false;

			Wall wall;
			bool isv = false;
			do
			{
				wall = new Wall((rand.Next(1, board), rand.Next(1, board)), isv = Convert.ToBoolean(rand.Next(2)));
			} while (walls.Any(w => w.Intersects(wall)));

			return base.TryPlaceWall(wall.Center, board, isv, ref walls);
		}

		// protected override Vector2D CalculateMove(int board, List<Wall> walls, Player opponent)
		// {
		// 	throw new NotImplementedException();
		// }
	}
}