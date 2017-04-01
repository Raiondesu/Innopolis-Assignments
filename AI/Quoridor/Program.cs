namespace Quoridor
{
	using Algorithms;

	class Program
	{
		static void Main(string[] args)
		{
			QuoridorGame.PlayGameOfTwo(
				new Player("Hillary", new MonteCarlo()),
				new Player("Donald", new MonteCarlo())
			);
		}
	}
}
