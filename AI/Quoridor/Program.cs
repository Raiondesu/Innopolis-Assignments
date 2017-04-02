namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			QuoridorGame.Play(
				new Player("Donald", typeof(Algorithms.AlphaBeta)),
				new Player("Hillary", typeof(Algorithms.AlphaBeta))
			);
		}
	}
}
