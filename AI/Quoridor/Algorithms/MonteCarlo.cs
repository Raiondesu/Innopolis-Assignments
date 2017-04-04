using System;

namespace Quoridor.Algorithms
{
	public class MonteCarlo : AI
	{
		public MonteCarlo(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + "Monte Carlo Tree Search)";
		public override string ShortName => base.ShortName + "MCTS)";
	}
}