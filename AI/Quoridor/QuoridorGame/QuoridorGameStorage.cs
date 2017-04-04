using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		public int BoardSize { get; }

		public List<Wall> AllWalls = new List<Wall>();
		public Player Ally { get; }
		public Player Opponent { get; }
	}
}