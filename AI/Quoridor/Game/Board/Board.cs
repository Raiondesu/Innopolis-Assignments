using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor
{
	public class Board
	{
		public Board(Player one, Player another, int size = Settings.BoardSize)
		{
			this.Size = (size << 1);

			var rand = new System.Random();
			var x = this.Size / 2;
			this.Players.Add(one.Name, one);
			this.Players[one.Name].Goal = 1;
			this.Players[one.Name].Location = (x, this.Size - 1);
			this.Players[one.Name].Color = Settings.PlayerColors[rand.Next(Settings.PlayerColors.Length)];

			this.Players.Add(another.Name, another);
			this.Players[another.Name].Goal = this.Size - 1;
			this.Players[another.Name].Location = (x, 1);
			this.Players[another.Name].Color = Settings.PlayerColors[rand.Next(Settings.PlayerColors.Length)];
			
			this.Players[one.Name].OpponentName = another.Name;
			this.Players[another.Name].OpponentName = one.Name;
		}

		public Board(Board toCopy)
		{
			this.Players = new Dictionary<string, Player>();
			foreach (var player in toCopy.Players)
				this.Players.Add(player.Key, player.Value.Copy());

			this.Walls = new List<Wall>();
			foreach (var wall in toCopy.Walls)
				this.Walls.Add(wall);

			this.Size = toCopy.Size;
		}

		public int Size { get; }
		public Player Ally => this.Players.First().Value;
		public Player Opponent => this.Players.Last().Value;
		public Player Winner => this.Players.SingleOrDefault(p => p.Value.HasWon).Value;
		public Dictionary<string, Player> Players = new Dictionary<string, Player>();
		public List<Wall> Walls = new List<Wall>();

		public bool HasWinner => this.Players.Any(p => p.Value.HasWon);
		
		public void BeginBattle(System.Action battle) => battle();
		
		public int Value
        {
            get
            {
                var oneLength = AStar.GetPathLenght(this.Ally.Location, this.Ally.Goal, this);
                var otherLength = AStar.GetPathLenght(this.Opponent.Location, this.Opponent.Goal, this);
                return oneLength - otherLength;
            }
        }
		

		public List<Action<Board>> AllMovesFor(Player player) => this.AllMovesFor(player.Name);
		
		private List<Action<Board>> AllMovesFor(string player)
		{
			var moves = new List<Action<Board>>();

            var s = new Board(this);

			void addMove(Vector2D direction, bool emergency)
			{
				if (s.Players[player].TryMove(direction, s, emergency))
					moves.Add((state) => {
						state.Players[player].TryMove(direction, state, emergency);
					});
			}

            void addMoves(Vector2D direction)
			{
				addMove(direction, true);
				addMove(direction, false);
			}

			addMoves(Directions.Up);
			addMoves(Directions.Down);
			addMoves(Directions.Left);
			addMoves(Directions.Right);

            for (int y = 2; y <= s.Size - 2; y += 2)
				for (int x = 2; x <= s.Size - 2; x += 2)
				{
					int _x = x;
					int _y = y;
					s = new Board(this);
					if (s.Players[player].TryPlaceWall((x, y), false, ref s))
						moves.Add((state) => {
							state.Players[player].TryPlaceWall((_x,  _y), false, ref state);
						});

					s = new Board(this);
					if (s.Players[player].TryPlaceWall((x, y), true, ref s))
						moves.Add((state) => {
							state.Players[player].TryPlaceWall((_x,  _y), true, ref state);
						});
				}

			return moves;
		}

		public override string ToString() => $"{Ally} | {Opponent} | {Walls.Count}";
	}
}