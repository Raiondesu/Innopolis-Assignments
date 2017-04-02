namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameStats = QuoridorGame.Play(9,
				("Donald", typeof(Algorithms.AlphaBeta)),
				("Hillary", typeof(Algorithms.MonteCarlo))
			); //US elections 2016 racing simulator

			System.Console.WriteLine($"Congrats to {gameStats.Winner}!");
		}
	}
}
