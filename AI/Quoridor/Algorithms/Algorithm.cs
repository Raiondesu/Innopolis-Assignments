namespace Quoridor.Algorithms
{
	public abstract class Algorithm
	{
		public Algorithm(Player user) => this.User = user;

		public abstract string Name { get; }
		public abstract string ShortName { get; }

		public Player User { get; protected set; }
	}

	public class Empty : Algorithm
	{
		public Empty(Player user) : base(user) {}

		public override string Name => null;
		public override string ShortName => null;
	}
}