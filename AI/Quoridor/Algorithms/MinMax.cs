using System;

namespace Quoridor.Algorithms
{
	public class MinMax : Player
	{
		public MinMax(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + " (Min-Max game tree)"; 

		public override void Turn(ref Board board, int delay = 0)
		{
			throw new NotImplementedException();
		}
	}
}