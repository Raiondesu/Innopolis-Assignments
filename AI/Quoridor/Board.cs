namespace Quoridor
{
	public class Board
	{
		private int _size;

		public Board(int size = 9)
		{
			this._size = (size << 1) - 1;
		}
	}
}