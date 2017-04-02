using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		public class Board
		{
			public Board(int size = Defaults.BoardSize)
			{
				this.Size = (size << 1) - 1;
				this.Size2D = new Vector2D(Size);
				this.Walls = new Dictionary<Player, List<Wall>>();
			}
			public int Size { get; }
			public Vector2D Size2D { get; }
			public Dictionary<Player, List<Wall>> Walls { get; private set; }
			
			public bool TryAddWall(Player player, Wall wall)
			{
				if (wall.Fits(this.Size2D) && !Walls.Values.All(l => l.Contains(wall)))
				{
					this.Walls[player].Add(wall);
					return true;
				} else return false;
			}

			public static readonly Board Empty = new Board();

			internal bool HasObstacleBetween(Player player, Vector2D loc)
			{
				var avg = (player.Location + loc) / 2;
				return this.Walls[player].Any(w => w.Center == avg || w.Start == avg || w.End == avg);
			}
		}

		private Board _board;

		public Player[] Players { get; private set; }
		public int BoardSize => this._board.Size;
		public Vector2D BoardSize2D => this._board.Size2D;
	}
}