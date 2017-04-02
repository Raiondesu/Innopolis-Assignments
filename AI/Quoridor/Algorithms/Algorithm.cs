namespace Quoridor.Algorithms
{
	public abstract class Algorithm
	{
		public Algorithm(Player user) => this.User = user;

		public abstract string Name { get; }
		public abstract string ShortName { get; }

		public Player User { get; protected set; }
	}
}