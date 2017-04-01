using System;
using System.Threading.Tasks;

namespace Quoridor
{
	public class QuoridorGame
	{
		private Task<Player> _getWinner;

		public QuoridorGame(params Player[] players)
		{
			this.Players = players;
		}

		public Player[] Players { get; private set; }
		public Player Winner => _getWinner.Result;

		public async void Play()
		{
			throw new System.NotImplementedException();
		}

		public static void PlayGameOfTwo(Player player1, Player player2)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			
			var game = new QuoridorGame(player1, player2);
			
			game.Play();	//Magic

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