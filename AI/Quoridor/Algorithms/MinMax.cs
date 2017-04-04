using System;

namespace Quoridor.Algorithms
{
	public class MinMax : AI
	{
		public MinMax(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name + "Min-Max game tree)"; 
		public override string ShortName => base.ShortName + "Min-Max GT)";
	}
}