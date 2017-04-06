using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quoridor
{
	public class Board
	{
		private Task<Player> battle;

		public Board(ref Player ally, ref Player opponent, int size = Settings.BoardSize)
		{
			this.Ally = ally;
			this.Opponent = opponent;
			this.Size = (size << 1);
		}

		public int Size { get; }
		public Player Ally { get; }
		public Player Opponent { get; }
		public List<Wall> Walls { get; } = new List<Wall>();

		public Player Winner => battle?.Result;
		public void BeginBattle(Func<Task<Player>> rules) => battle = rules();
	}
}