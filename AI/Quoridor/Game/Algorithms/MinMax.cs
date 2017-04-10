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
				Moves = this.Moves,
				Goal = this.Goal,
				Steps = this.Steps,
				Location = this.Location,
				OldLocation = this.OldLocation,
				OpponentName = this.OpponentName
			};
		}

		public override void Turn(ref Board board, int delay = 0)
		{
			var allMoves = board.AllMovesFor(this);
			var bestMove = allMoves[0];
			int bestValue = int.MinValue;
			int multiplier = this.Goal - 1 == 0 ? -1 : 1;

			foreach (var currentMove in allMoves)
			{
				var temp = new Board(board);
				currentMove(temp);
				var value = temp.Value * multiplier;//this.Calculate(temp, this.OpponentName, this.Depth - 1) * multiplier;
				if (value >= bestValue)
				{
					bestValue = value;
					bestMove = currentMove;
				}
			}

			this.Moves.Add(bestMove);
			bestMove(board);

			base.Turn(ref board, delay);
		}

		private int Calculate(Board board, string activePlayer, int depth)
		{
			var tempBoard = new Board(board);
			var player = tempBoard.Players[activePlayer];
			
			if (depth == 0) return tempBoard.Value;

			var newMoves = tempBoard.AllMovesFor(player);

			int bestValue = int.MinValue;
			for (int i = 0; i < newMoves.Count; i++)
			{
				var temp = new Board(tempBoard);
				newMoves[i](temp);
				int value = Calculate(temp, player.OpponentName, depth - 1);
				if (value > bestValue)
					bestValue = value;
			}
			return bestValue;
		}
	}
}