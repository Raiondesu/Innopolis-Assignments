namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameStats = QuoridorGame.Play(new Board(),
				("Donald", typeof(Algorithms.AlphaBeta)),
				("Hillary", typeof(Algorithms.AlphaBeta))
			);

			System.Console.WriteLine(gameStats.Winner.Name);
		}
	}
}
