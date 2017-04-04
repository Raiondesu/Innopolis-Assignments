namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			var gameStats = QuoridorGame.Play(
				new Algorithms.Random("Donald", Settings.Depth),
				new Algorithms.Random("Hillary", Settings.Depth)
			); //US elections 2016 racing simulator

			System.Console.WriteLine($"Congrats to {gameStats.Winner}!");
		}
	}
}
