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
	}
}