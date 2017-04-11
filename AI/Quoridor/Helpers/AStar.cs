using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quoridor
{
	static class AStar
	{
		// public static List<Vector2D> FindPath(Vector2D from, Vector2D to, Board board)
		public static bool HasPath(Vector2D from, int to, Board board) => GetPathLength(from, to, board) > -1;

		public static int GetPathLength(Vector2D from, int to, Board board)
		{
			Collection<Node> closed = new Collection<Node>();
			Collection<Node> open = new Collection<Node>();

			Node start = new Node
			{
				Position = from,
				Previous = null,
				StepsFromStart = 0,
				EstimatedRemainingPath = EstimatePathLength(from, to)
			};

			open.Add(start);

			while (open.Count > 0)
			{
				var current = open.First();
				if (current.Position.Y == to)
					// return GetPathFor(current);
					return current.StepsFromStart;
				open.Remove(current);
				closed.Add(current);

				var near = Neighbours(current, to, board);
				foreach (var neighbour in near)
				{
					if (closed.Count(node => node.Position == neighbour.Position) > 0)
						continue;

					var openNode = open.FirstOrDefault(node => node.Position == neighbour.Position);

					if (openNode == null)
						open.Add(neighbour);
					else if (openNode.StepsFromStart > neighbour.StepsFromStart)
					{
						openNode.Previous = current;
						openNode.EstimatedRemainingPath = current.EstimatedRemainingPath;
					}
				}
			}
			// return null;
			return -1;
		}

		// private static int GetPathFor(Node current)
		// {
		// 	var result = new List<Vector2D>();
		// 	var temp = current;

		// 	while (temp != null)
		// 	{
		// 		result.Add(temp.Position);
		// 		temp = temp.Previous;
		// 	}
		// 	result.Reverse();

		// 	return current.StepsFromStart;
		// }

		private static int EstimatePathLength(Vector2D from, Vector2D to)
			=> Abs(from.X - to.X) + Abs(from.Y - to.Y);
			
		public static int Abs(int x) => (x ^ (x >> 31)) - (x >> 31);

		private static List<Node> Neighbours(Node node, Vector2D goal, Board board)
		{
			var result = new List<Node>();

			int x = node.Position.X;
			int y = node.Position.Y;

            var p = new Vector2D(x, y);

			var near = new List<Vector2D>();

			near.AddRange(new Vector2D[] {
				(x + 2, y),
				(x - 2, y),
				(x, y + 2),
				(x, y - 2),
			}.Where(v => v.FitsIn(board.Size)));


			for (int i = 0; i < near.Count; i++)
            {
                var v = near[i];

                var vector = (p + ((v - p) / 2));
                
                if (board.Walls.Any(w => w.LiesOn(vector)))
                {
                    near.RemoveAt(i);
                    continue;
                }

				result.Add(new Node	{
					Position = v,
					Previous = node,
					StepsFromStart = node.StepsFromStart + 1,
					EstimatedRemainingPath = EstimatePathLength(v, goal)
				});
			}

			return result;
		}

		private class Node
		{
			public Vector2D Position { get; set; }
			public int StepsFromStart { get; set; }
			public Node Previous { get; set; }
			public int EstimatedRemainingPath { get; set; }
			public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
            public override string ToString() => Position.ToString();
		}
	}
}