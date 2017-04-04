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
			this.BoardSize = (Settings.BoardSize << 1) - 1;
			
			var rand = new System.Random();
			var x = BoardSize / 2 + 1;

			ally.Location = (x, BoardSize);
			ally.Color = (ConsoleColor) rand.Next(1, 16);
			opponent.Location = (x, 1);
			opponent.Color = (ConsoleColor) rand.Next(1, 16);

			ally.Print();
			opponent.Print();
		}

		private async Task<Player> PlayAsync()
		{
			

			//TODO - make'em actually play... v_v
			await Task.Delay(1000);
			return Player.Nobody;
		}

		public static QuoridorGame Play(Player ally, Player opponent)
		{
			Console.WriteLine("Hello, spectator!");
			Console.WriteLine("We're starting the Quoridor Game!");
			
			var game = new QuoridorGame(ally, opponent);
			
			game._battle = game.PlayAsync();	//Magic

			Console.WriteLine("Started a huuuge battle between {ally.Name} and {opponent.Name}!");
			Console.WriteLine("So, who's gonna win???");

			Console.WriteLine($"The winner is...\n{game.Winner.Name}!!!");

			return game;
		}
	}
}