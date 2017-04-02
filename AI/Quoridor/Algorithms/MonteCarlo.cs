namespace Quoridor.Algorithms
{
	public class MonteCarlo : Algorithm
	{
		public MonteCarlo(Player user)
		{
			this.User = user;
		}

		public override string Name => "Monte Carlo Tree Search";
		public override string ShortName => "MCTS";
	}
}