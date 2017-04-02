using System;
using System.Threading.Tasks;

namespace Quoridor
{
	public class QuoridorGame
	{
		private Task<Player> _battle;

		private QuoridorGame(Board board, params (string Name, Type Algorithm)[] players)
		{
			this.GameBoard = board;
			this.Players = new Player[players.Length];
			for (int i = 0; i < players.Length; i++)
				this.Players[i] = new Player(players[i].Name, players[i].Algorithm);
		}

		public Player[] Players { get; private set; }
		public Board GameBoard { get; private set; }
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

		public static QuoridorGame Play(Board board, params (string Name, Type Algorithm)[] players)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			
			var game = new QuoridorGame(board, players);
			
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

			return game;
		}

		private async Task<Player> PlayInternal()
		{
			await Task.Delay(1000);
			return this.Players[0];
		}
	}
}