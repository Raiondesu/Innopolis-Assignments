using System;
using System.Collections.Generic;
using Force.DeepCloner;

namespace Learning.Engine
{
	public class Map
	{
		public Map(Vector2D size)
		{
			this.Size = size;
		}

		public Map(int size) : this(new Vector2D(size)) {}

		public Map Copy()
		{
			return new Map(this.Size) {
				Actors = this.Actors.DeepClone()
			};
		}

		public Vector2D Size { get; private set; }
		public List<Actor> Actors { get; private set; } = new List<Actor>();
		public Player Player => Actors.Find(a => a is Player) as Player;

		public List<(Map s, Func<int> a)> GetAllPossiblePlayerStates()
		{
			var result = new List<(Map s, Func<int> a)>();

			var copy = this.Copy();
			var player = copy.Player;
			MoveInternal(ref result, copy, player, player.MoveUp);
			copy = this.Copy();
			player = copy.Player;
			MoveInternal(ref result, copy, player, player.MoveDown);
			copy = this.Copy();
			player = copy.Player;
			MoveInternal(ref result, copy, player, player.MoveLeft);
			copy = this.Copy();
			player = copy.Player;
			MoveInternal(ref result, copy, player, player.MoveRight);

			return result;
		}

		private void MoveInternal(ref List<(Map s, Func<int> a)> result, Map state, Player player, Func<Map, int> move)
		{
			var current = state.Copy();
			int idx = move(state);
			if (idx != -1)
			{
				var copy = current.Copy();
				(Map s, Func<int> a) item = (state.Copy(), () => move(copy));
				if (idx == 1)
					result = new List<(Map s, Func<int> a)>() { item };
				else if (idx == 0)
					result.Add(item);
			}
		}

		public override string ToString() => $"{this.Actors.Count} - {this.Actors.Find(a => a is Player).Location}";
	}
}