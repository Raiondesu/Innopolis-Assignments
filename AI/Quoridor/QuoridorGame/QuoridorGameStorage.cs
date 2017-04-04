using System.Collections.Generic;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		public int BoardSize { get; }

		public List<Wall> Walls = new List<Wall>();
		public Player Ally { get; }
		public Player Opponent { get; }
	}
}