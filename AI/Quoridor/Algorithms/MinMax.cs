namespace Quoridor.Algorithms
{
	public class MinMax : Algorithm
	{
		public MinMax(Player user) : base(user) {}

		public override string Name => "Min-Max game tree"; 
		public override string ShortName => "Min-Max GT"; 
	}
}