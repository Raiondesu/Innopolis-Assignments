using System;

namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, Tester!");
			Console.WriteLine("Starting the Quoridor Game...");
			var game = new QuoridorGame(
				new Player("Hillary", new MonteCarlo()),
				new Player("Donald", new MonteCarlo())
			);
			
			game.Play();

			Console.Write("Started a huuge play between ");
			for (int i = 0; i < game.Players.Length; i++)
			{
				if (i == game.Players.Length - 1) Console.Write(" and ");
				else if (i > 0) Console.Write(", ");
				var player = game.Players[i];
				Console.Write($"{player.Name} ({player.PlayAlgorithm.ShortName})");
			}
			Console.WriteLine("!");
			Console.WriteLine("So, who's gonna win???");

			Console.WriteLine($"The winner is... {game.Winner.Name}!!!");
		}
	}
}
