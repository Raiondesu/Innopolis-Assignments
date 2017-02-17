using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace EscapeMission
{
	public class ASStar
	{
		public static List<Matrix.Cell> FindPath(Matrix.Cell from, Matrix.Cell to, Matrix.Cell[,,] field)
		{
			var closed = new List<Node>();
			var open = new FastPriorityQueue<Node>(10000);

			Node start = new Node {
				Location = to.Location,
				Next = null,
				StepsFromStart = 0,
				EstimatedRemainingPath = FastEstimatePathLength(to.Location, from.Location)
			};

			open.Enqueue(start, start.FullPathLength);

			while (open.Count > 0)
			{
				var current = open.Dequeue();
				if (current.Location == from.Location)
					return GetPathFor(current, field);

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
			return null;
		}

		private static List<Matrix.Cell> GetPathFor(Node current, Matrix.Cell[,,] field)
		{
			var result = new List<Matrix.Cell>();

			for (var t = current.Next; t != null; t = t.Next)
				result.Add(field[t.Location.X, t.Location.Y, t.Location.Z]);

			return result;
		}

		public static int FastEstimatePathLength(Vector from, Vector to)
//			=> 0;
//			=> ((from.X * from.X - to.X * to.X) + (from.Y * from.Y - to.Y * to.Y) + (from.Z * from.Z - to.Z * to.Z));

		//...
//		public static int SlowEstimatePathLength(Vector from, Vector to)
			=> Abs(from.X - to.X) + Abs(from.Y - to.Y) + Abs(from.Z - to.Z);
//
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
				    && v.Z < field.GetLength(2) && v.Z >= 0
				    && field[v.X, v.Y, v.Z].IsEmpty)
					yield return new Node {
						Location = v,
						Next = node,
						StepsFromStart = node.StepsFromStart + 1,
						EstimatedRemainingPath = FastEstimatePathLength(v, goal)
					};
		}

		private class Node : FastPriorityQueueNode
		{
			public Vector Location { get; set; }
			public int StepsFromStart { get; set; }
			public Node Next { get; set; }
			public int EstimatedRemainingPath { get; set; }
			public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
		}
	}
}