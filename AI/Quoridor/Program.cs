namespace Quoridor
{
	class Program
	{
		static void Main(string[] args)
		{
			var game = Game.Play(
				new Algorithms.MinMax("Donald"),
				new Algorithms.MinMax("Hillary")
			); //US elections 2016 racing simulator
		}
	}
}
