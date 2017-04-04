namespace Quoridor.Algorithms
{
	public abstract class AI : Player
	{
		public AI(string name, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}
		
		public override string Name => base.Name + " (AI, ";
		public virtual string ShortName => Name;
	}
}