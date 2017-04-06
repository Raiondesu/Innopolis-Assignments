using System;
using System.Threading.Tasks;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		private Board board;

		private QuoridorGame(ref Player ally, ref Player opponent)
		{
			this.board = new Board(ref ally, ref opponent);
			var rand = new System.Random();
			var x = this.board.Size / 2;

			this.board.Ally.Color = Settings.PlayerColors[rand.Next(Settings.PlayerColors.Length)];
			this.board.Ally.Location = (x, this.board.Size - 1);
			this.board.Opponent.Color = Settings.PlayerColors[rand.Next(Settings.PlayerColors.Length)];
			this.board.Opponent.Location = (x, 1);
		}

		~QuoridorGame() => Console.CursorVisible = true;

		private Player Battle()
		{
			Player checkWinner()
			{
				if (board.Ally.Location.Y == 1)
					return board.Ally;
				else if (board.Opponent.Location.Y == board.Size - 1)
					return board.Opponent;
				else
					return null;
			}
			bool hasWinner() => board.Ally.Location.Y == 1 || board.Opponent.Location.Y == board.Size - 1;

			do
			{
				board.Ally.Turn(ref this.board, 500);
				board.Opponent.Turn(ref this.board, 500);
			} while (!hasWinner());

			return checkWinner();
		}

		public static QuoridorGame Play(Player ally, Player opponent)
		{
			Console.Clear();
			Console.Title = "Quoridor Game";
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			Console.WriteLine($"A huuuge battle between {ally.Name} and {opponent.Name}!");
			Console.WriteLine("So, who's gonna win???");
			
			var game = new QuoridorGame(ref ally, ref opponent);
			
			game.board.BeginBattle(game.Battle);	//Magic

			Console.ForegroundColor = game.board.Winner?.Color ?? ConsoleColor.White;
			Console.WriteLine($"The winner is... {game.board.Winner?.Name ?? "Nobody"}!!!");
			Console.ResetColor();
			return game;
		}
	}
}