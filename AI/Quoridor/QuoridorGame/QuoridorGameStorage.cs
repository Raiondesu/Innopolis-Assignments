using System.Collections.Generic;
using System.Collections.Immutable;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		public int BoardSize { get; }

		private List<Wall> walls = new List<Wall>();
		public ImmutableList<Wall> Walls => walls.ToImmutableList();
		public Player Ally { get; }
		public Player Opponent { get; }
	}
}