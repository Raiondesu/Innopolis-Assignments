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

		public override bool Turn(ref Board board)
		{
			var opponent = object.ReferenceEquals(this, board.Ally) ? board.Opponent : board.Ally;
			return rand.Next(2) > 0 ?
				this.TryMove(board.Size, board.Walls, opponent)
				: this.TryPlaceWall(ref board);
		}

		protected bool TryMove(int board, List<Wall> walls, Player opponent)
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

		protected bool TryPlaceWall(ref Board board)
		{
			if (this.WallsAmount == 0) return false;

			Wall wall;
			bool isv = false;
			do
			{
				wall = new Wall((rand.Next(1, board.Size), rand.Next(1, board.Size)), isv = Convert.ToBoolean(rand.Next(2)));
			} while (board.Walls.Any(w => w.Intersects(wall)));

			return base.TryPlaceWall(wall.Center, isv, ref board);
		}
	}
}