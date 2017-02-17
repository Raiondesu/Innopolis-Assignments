using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace EscapeMission
{
	public class ASStar
	{
		public static List<Matrix.Cell> FindPath(Matrix.Cell from, Matrix.Cell to, Matrix.Cell[,,] field, out Vector boom)
		{
			var closed = new List<Node>();
			var open = new FastPriorityQueue<Node>(10000);

			Node start = new Node {
				Location = to.Location,
				Next = null,
				StepsFromStart = 0,
				EstimatedRemainingPath = EstimatePathLength(to.Location, from.Location)
			};

			open.Enqueue(start, start.FullPathLength);

			while (open.Count > 0)
			{
				var current = open.Dequeue();
				if (current.Location == from.Location)
					return GetPathFor(current, field, out boom);

				closed.Add(current);

				var near = Neighbours(current, from.Location, field);
				foreach (var neighbour in near)
				{
					if (closed.Any(node => node.Location == neighbour.Location))
						continue;

					var openNode = open.FirstOrDefault(node => node.Location == neighbour.Location);

					if (openNode == null)
						open.Enqueue(neighbour, neighbour.FullPathLength);
					else if (openNode.StepsFromStart > neighbour.StepsFromStart)
					{
						openNode.Next = current;
						openNode.EstimatedRemainingPath = current.EstimatedRemainingPath;
					}
				}
			}
			boom = null;
			return null;
		}

		private static List<Matrix.Cell> GetPathFor(Node current, Matrix.Cell[,,] field, out Vector boom)
		{
			boom = null;
			var result = new List<Matrix.Cell>();

			for (var t = current.Next; t.Next != null; t = t.Next)
			{
				if (!t.CanBoom) boom = t.Next.Location;
				result.Add(field[t.Location.X, t.Location.Y, t.Location.Z]);
			}

			return result;
		}

		/// <summary>
		/// Heruistic Function with possibilities to be uncommented...
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		public static int EstimatePathLength(Vector from, Vector to)
//			=> 0;
//			=> ((from.X * from.X - to.X * to.X) + (from.Y * from.Y - to.Y * to.Y) + (from.Z * from.Z - to.Z * to.Z));
			=> Abs(from.X - to.X) + Abs(from.Y - to.Y) + Abs(from.Z - to.Z);

		/// <summary>
		/// Just a fast Abs function. ~2500 times faster than Math.Abs.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static int Abs(int x) => (x ^ (x >> 31)) - (x >> 31);

		private static IEnumerable<Node> Neighbours(Node node, Vector goal, Matrix.Cell[,,] field)
		{
			int x = node.Location.X;
			int y = node.Location.Y;
			int z = node.Location.Z;

			var near = new List<Vector> {
				new Vector(x + 1, y, z),
				new Vector(x - 1, y, z),
				new Vector(x, y + 1, z),
				new Vector(x, y - 1, z),
				new Vector(x, y, z + 1),
				new Vector(x, y, z - 1)
			};

			foreach (var v in near)
				if (v.X < field.GetLength(0) && v.X >= 0
				    && v.Y < field.GetLength(1) && v.Y >= 0
				    && v.Z < field.GetLength(2) && v.Z >= 0)
				{
					if ((field[v.X, v.Y, v.Z].HasKraken && node.CanBoom) || field[v.X, v.Y, v.Z].IsEmpty)
					{
						if (field[v.X, v.Y, v.Z].HasKraken && node.CanBoom) // For bombs. Important.
							node.CanBoom = false;
						yield return new Node {
							Location = v,
							Next = node,
							CanBoom = node.CanBoom,
							StepsFromStart = node.StepsFromStart + 4,
							EstimatedRemainingPath = EstimatePathLength(v, goal)
						};
					}
				}
		}

		private class Node : FastPriorityQueueNode
		{
			public Vector Location { get; set; }
			public Node Next { get; set; }
			public bool CanBoom { get; set; } = true;
			public int StepsFromStart { get; set; }
			public int EstimatedRemainingPath { get; set; }
			public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
		}
	}
}