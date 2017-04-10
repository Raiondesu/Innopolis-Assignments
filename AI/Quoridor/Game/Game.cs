using System;

namespace Quoridor
{
	public partial class Game
	{
		private Board board;
		public Board Stats => this.board;

		Game(Player one, Player another)
		{
			if (one.Name == another.Name)
				throw new ArgumentException("Players names' must be unique!");

			this.board = new Board(one, another);

			if (Settings.Log)
			{
				Console.OutputEncoding = System.Text.Encoding.UTF8;
				Console.CursorVisible = false;
				this.PrintFieldInline(Settings.FieldColor);
				board.Players.Values.ForEach(p => {
					this.Print(p);
					this.board.Walls.ForEach(this.Print);
				});
			}
		}
		~Game() => Console.CursorVisible = true;

		private void Battle()
		{
			while (!board.HasWinner)
			{
				board.Players.Values.ForEach(p => {
					if (!board.HasWinner)
					{
						p.Turn(ref this.board);
						this.Print(p);
						this.board.Walls.ForEach(this.Print);
					}
				});
			}
		}

		public static Game Play(Player one, Player another)
		{
			if (Settings.Log)
			{
				Console.Clear();
				Console.Title = "Quoridor Game";
				Console.WriteLine("Hello, spectator!");
				Console.WriteLine("We're starting the Quoridor Game!");
				Console.WriteLine($"A huuuge battle between {one.Name} and {another.Name}!");
				Console.WriteLine("So, who's gonna win???");
			}
			
			var game = new Game(one, another);
			
			game.board.BeginBattle(game.Battle);	//Magic

			if (Settings.Log)
			{
				Console.ForegroundColor = game.Stats.Winner.Color;
				Console.WriteLine($"The winner is... {game.Stats.Winner.Name}!!!");
				Console.ResetColor();
			}
			return game;
		}
	}
}