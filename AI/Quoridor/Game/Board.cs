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
			this.Players[one.Name].IsMaximizing = true;
			this.Players[one.Name].Location = (x, this.Size - 1);
			this.Players[one.Name].Color = Settings.PlayerColors[rand.Next(Settings.PlayerColors.Length)];

			this.Players.Add(another.Name, another);
			this.Players[another.Name].Goal = this.Size - 1;
			this.Players[another.Name].IsMaximizing = false;
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
		
		public int Value(string myName, string opponentName)
        {
			var oneLength = AStar.GetPathLength(this.Players[myName].Location, this.Players[myName].Goal, this);
			var otherLength = AStar.GetPathLength(this.Players[opponentName].Location, this.Players[opponentName].Goal, this);
			return oneLength - otherLength;
        }

		public override string ToString() => $"{Ally} | {Opponent} | {Walls.Count}";
	}
}