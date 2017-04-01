namespace Quoridor
{
	public class AlphaBeta : MinMax
	{
		public override string Name => base.Name + " with aplha-beta prunning";
		public override string ShortName => base.ShortName + " w/ABP";
	}
}