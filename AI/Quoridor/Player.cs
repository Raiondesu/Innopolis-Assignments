namespace Quoridor
{
	public class Player
	{
		public Player(string name, Algorithm algorithm, int wallsAmount = Defaults.WallsAmount)
		{
			this.Name = name;
			this.WallsAmount = wallsAmount;
			this.PlayAlgorithm = algorithm;
		}

		public string Name { get; private set; }
		public int WallsAmount { get; private set; }
		public Algorithm PlayAlgorithm { get; private set; }
	}
}