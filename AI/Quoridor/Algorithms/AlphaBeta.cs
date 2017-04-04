namespace Quoridor.Algorithms
{
	public class AlphaBeta : MinMax
	{
		public AlphaBeta(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override string Name => base.Name.TrimEnd(')') + " with aplha-beta pruning)";
	}
}