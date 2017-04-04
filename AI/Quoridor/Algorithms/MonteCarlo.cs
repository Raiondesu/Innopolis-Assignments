using System;
using System.Collections.Generic;

namespace Quoridor.Algorithms
{
	public class MonteCarlo : Player
	{
		public MonteCarlo(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + " (Monte Carlo Tree Search)";

		public override bool Turn(int board, ref List<Wall> walls, Player opponent)
		{
			throw new NotImplementedException();
		}
	}
}