namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameStats = QuoridorGame.Play(
				new Algorithms.AlphaBeta("Donald", Settings.Depth),
				new Algorithms.MonteCarlo("Hillary", Settings.Depth)
			); //US elections 2016 racing simulator

			System.Console.WriteLine($"Congrats to {gameStats.Winner}!");
		}
	}
}
