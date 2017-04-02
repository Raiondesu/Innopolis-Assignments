namespace Quoridor.Algorithms
{
	public class MonteCarlo : Algorithm
	{
		public MonteCarlo(Player user) : base(user) {}

		public override string Name => "Monte Carlo Tree Search";
		public override string ShortName => "MCTS";
	}
}