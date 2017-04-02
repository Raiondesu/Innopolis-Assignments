namespace Quoridor.Algorithms
{
	public class AlphaBeta : MinMax
	{
		public AlphaBeta(Player user) : base(user) {}

		public override string Name => base.Name + " with aplha-beta pruning";
		public override string ShortName => base.ShortName + " w/ABP";
	}
}