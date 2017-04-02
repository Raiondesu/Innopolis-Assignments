using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		private Task<Player> _battle;
		public Player Winner => _battle?.Result ?? Player.Nobody;

		private QuoridorGame(int boardSize, params (string Name, Type Algorithm)[] players)
		{
			if (players.Length % 2 != 0)
				throw new ArgumentException("Amount of players must be even!");

			this._board = new Board(boardSize);
			this.Players = new Player[players.Length];
			for (int i = 0; i < players.Length; i++)
			{
				this.Players[i] = new Player(players[i].Name, players[i].Algorithm, ref this._board);
				this._board.Walls.Add(this.Players[i], new List<Wall>());
			}

		}

		private void Divide(Player[] players, int lane, int y)
		{
			int step = lane / (players.Length + (players.Length % 2));

			for (int i = 0; i < players.Length; i++)
				players[i].SetInitial(new Vector2D(step * (i + 1), y));
		}

		private async Task<Player> PlayAsync()
		{
			Divide(Players.Take(Players.Length / 2).ToArray(), (BoardSize + 1), 1);
			Divide(Players.Skip(Players.Length / 2).ToArray(), (BoardSize + 1), BoardSize);
			
			this.Players[0].PlaceWall(new Wall(10, 12, true));
			this.Players[1].PlaceWall(new Wall(10, 2));
			this.Players[0].Step(Player.Up);
			this.Players[1].PlaceWall(new Wall(6, 2));
			this.Players[0].PlaceWall(new Wall(6, 16));
			this.Players[1].Step(Player.Down);
			this.Players[0].Step(Player.Right);
			this.Players[1].Step(Player.Down);
			this.Players[0].PlaceWall(new Wall(2, 12));
			this.Players[0].PlaceWall(new Wall(6, 12));
			this.Players[0].PlaceWall(new Wall(6, 12));
			this.Players[0].PlaceWall(new Wall(8, 12));
			this.Players[0].PlaceWall(new Wall(10, 12));
			this.Players[0].PlaceWall(new Wall(12, 12));
			this.Players[0].PlaceWall(new Wall(14, 12));
			this.Players[0].PlaceWall(new Wall(16, 12));
			this.Players[0].PlaceWall(new Wall(18, 12));
			this.Print();

			//TODO - make'em actually play... v_v
			await Task.Delay(1000);
			return this.Players.Any(p => p.HasWon) ? this.Players.First(p => p.HasWon) : Player.Nobody;
		}

		public static QuoridorGame Play(int board, params (string Name, Type Algorithm)[] players)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			
			var game = new QuoridorGame(board, players);
			
			game._battle = game.PlayAsync();	//Magic

			Console.Write("Started a huuuge battle between ");
			for (int i = 0; i < game.Players.Length; i++)
			{
				if (i == game.Players.Length - 1) Console.Write(" and ");
				else if (i > 0) Console.Write(", ");
				var player = game.Players[i];
				Console.Write(player);
			}
			Console.WriteLine("!\nSo, who's gonna win???");

			Console.WriteLine($"The winner is...\n{game.Winner.Name}!!!");

			return game;
		}
	}
}