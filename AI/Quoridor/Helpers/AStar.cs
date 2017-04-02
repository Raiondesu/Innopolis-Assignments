using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Quoridor
{
	class AStar
	{
		public static (List<Vector2D> path, int length) FindPath(Vector2D from, Vector2D to, Vector2D bounds)
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
				var current = open.OrderBy(n => n.EstimatedRemainingPath).First();
				if (current.Position == to)
					return GetPathFor(current);
				open.Remove(current);
				closed.Add(current);

				var near = Neighbours(current, to, bounds);
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
			return (null, 0);
		}

		private static (List<Vector2D> path, int length) GetPathFor(Node current)
		{
			var result = new List<Vector2D>();
			var temp = current;

			while (temp != null)
			{
				result.Add(temp.Position);
				temp = temp.Previous;
			}
			result.Reverse();

			return (result, current.StepsFromStart);
		}

		private static int EstimatePathLength(Vector2D from, Vector2D to)
			=> Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);

		private static List<Node> Neighbours(Node node, Vector2D goal, Vector2D bounds)
		{
			var result = new List<Node>();

			int x = node.Position.X;
			int y = node.Position.Y;

			var near = new List<Vector2D>();

			near.AddRange(new [] {
				new Vector2D(x + 1, y),
				new Vector2D(x - 1, y),
				new Vector2D(x, y + 1),
				new Vector2D(x, y - 1),
			});

			for (int i = 0; i < near.Count; i++)
			{
				var v = near[i];
				if (!v.FitsIn(bounds)) near.Remove(v);

				result.Add(new Node
				{
					Position = v,
					Previous = node,
					StepsFromStart = node.StepsFromStart + 1,
					EstimatedRemainingPath = EstimatePathLength(v, goal)
				});
			}

			return result;
		}
	}

	public class Node
	{
		public Vector2D Position { get; set; }
		public int StepsFromStart { get; set; }
		public Node Previous { get; set; }
		public int EstimatedRemainingPath { get; set; }
		public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
	}
}