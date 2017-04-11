namespace Quoridor.Algorithms
{
	public class MinMax : Player
	{
		public MinMax(string name = null, int depth = Settings.Depth, int wallsAmount = Settings.WallsAmount)
			: base(name, depth, wallsAmount) {}

		public override Player Copy()
		{
			return new MinMax(this.Name, this.Depth, this.WallsAmount)
			{
				Goal = this.Goal,
				Steps = this.Steps,
				Location = this.Location,
				OldLocation = this.OldLocation,
				OpponentName = this.OpponentName,
				IsMaximizing = this.IsMaximizing
			};
		}

		public override void Turn(ref Board board, int delay = 0)
		{
			var allMoves = this.AllMovesOn(board);
			var bestMove = allMoves[0];
			int bestValue = int.MinValue;

			foreach (var currentMove in allMoves)
			{
				var temp = new Board(board);
				currentMove(temp);
				var value = this.Calculate(temp, this.OpponentName, this.Name, this.Depth - 1, !this.IsMaximizing);
				if (value > bestValue)
				{
					bestValue = value;
					bestMove = currentMove;
				}
			}

			bestMove(board);

			base.Turn(ref board, delay);
		}

		private int Calculate(Board board, string activePlayer, string nextPlayer, int depth, bool isMax)
		{
			if (depth == 0)
				return board.Value(activePlayer, nextPlayer);

			var newMoves = board.Players[activePlayer].AllMovesOn(board);

			int bestValue = isMax ? int.MinValue : int.MaxValue;
			foreach (var move in newMoves)
			{
				var temp = new Board(board);
				move(temp);
				int value = Calculate(temp, nextPlayer, activePlayer, depth - 1, !isMax);
				if (isMax ? value > bestValue : value < bestValue)
					bestValue = value;
			}
			return bestValue;
		}
	}
}