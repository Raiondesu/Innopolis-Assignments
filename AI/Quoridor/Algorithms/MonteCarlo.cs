using System;
using System.Collections.Generic;

namespace Quoridor.Algorithms
{
	public class MonteCarlo : Player
	{
		public MonteCarlo(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + " (Monte Carlo Tree Search)";

		public override void Turn(ref Board board, int delay)
		{
			throw new NotImplementedException();
		}
	}
}