using System;
using System.Threading.Tasks;

namespace Quoridor
{
	public partial class QuoridorGame
	{
		private Task<Player> _battle;
		public Player Winner => _battle?.Result ?? Player.Nobody;

		private QuoridorGame(Player ally, Player opponent)
		{
			this.BoardSize = (Settings.BoardSize << 1);
			
			var rand = new System.Random();
			var x = BoardSize / 2;

			ally.Color = (ConsoleColor) rand.Next(1, 16);
			ally.Location = (x, BoardSize - 1);
			opponent.Color = (ConsoleColor) rand.Next(1, 16);
			opponent.Location = (x, 1);

			this.Ally = ally;
			this.Opponent = opponent;
		}

		private async Task<Player> PlayAsync()
		{
			//TODO - make'em actually play... v_v
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Opponent.TryMove(Directions.Down, BoardSize, Walls, Ally);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Opponent.TryMove(Directions.Down, BoardSize, Walls, Ally);
			Ally.TryPlaceWall((8, 6), false, ref Walls);
			Opponent.TryPlaceWall((8, 8), true, ref Walls);
			Opponent.TryMove(Directions.Down, BoardSize, Walls, Ally);
			Ally.TryPlaceWall((10, 4), true, ref Walls);
			Opponent.TryPlaceWall((12, 6), false, ref Walls);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Opponent.TryPlaceWall((10, 8), true, ref Walls);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			Ally.TryMove(Directions.Up, BoardSize, Walls, Opponent);
			await Task.Delay(1000);
			return Player.Nobody;
		}

		public static QuoridorGame Play(Player ally, Player opponent)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			Console.WriteLine($"A huuuge battle between {ally.Name} and {opponent.Name}!");
			Console.WriteLine("So, who's gonna win???");
			
			var game = new QuoridorGame(ally, opponent);
			
			game._battle = game.PlayAsync();	//Magic

			Console.ForegroundColor = game.Winner.Color;
			Console.WriteLine($"The winner is...\n{game.Winner.Name}!!!");
			Console.ResetColor();
			return game;
		}
	}
}