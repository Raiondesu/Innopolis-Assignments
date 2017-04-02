namespace Quoridor
{
	public class Board
	{
		public Board(int size = 9)
			=> this.Size = (size << 1) - 1;
		
		public int Size { get; private set; }
		public Vector2D Size2D => new Vector2D(this.Size);
	}
}