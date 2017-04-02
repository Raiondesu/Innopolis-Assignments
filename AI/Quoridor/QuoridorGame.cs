using System;
using System.Threading.Tasks;

namespace Quoridor
{
	public class QuoridorGame
	{
		private Task<Player> _battle;

		public QuoridorGame(params Player[] players)
		{
			this.Players = players;
		}

		public Player[] Players { get; private set; }
		public Player Winner
		{
			get
			{
				if (_battle == null)
					return new Player("Nobody", null);
				_battle.Wait();
				return _battle.Result;
			}
		}

		public static void Play(params Player[] players)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			
			var game = new QuoridorGame(players);
			
			game._battle = game.PlayInternal();	//Magic

			Console.Write("Started a huuge play between ");
			for (int i = 0; i < game.Players.Length; i++)
			{
				if (i == game.Players.Length - 1) Console.Write(" and ");
				else if (i > 0) Console.Write(", ");
				var player = game.Players[i];
				Console.Write($"{player.Name} ({player.PlayAlgorithm.ShortName})");
			}
			Console.WriteLine("!\nSo, who's gonna win???");

			Console.WriteLine($"The winner is...\n{game.Winner.Name}!!!");
		}

		private async Task<Player> PlayInternal()
		{
			await Task.Delay(1000);
			return this.Players[0];
		}
	}
}