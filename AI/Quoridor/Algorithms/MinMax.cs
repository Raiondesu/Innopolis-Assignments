using System;
using System.Collections.Generic;

namespace Quoridor.Algorithms
{
	public class MinMax : Player
	{
		public MinMax(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + " (Min-Max game tree)"; 

		public override bool Turn(ref Board board)
		{
			throw new NotImplementedException();
		}
	}
}