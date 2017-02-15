using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EscapeMission
{
	struct ASStar
	{
		public static List<Matrix.Cell> FindPath(Matrix.Cell from, Matrix.Cell to, Matrix.Cell[,,] field)
		{
			Collection<Node> closed = new Collection<Node>();
			Collection<Node> open = new Collection<Node>();

			Node start = new Node {
				Location = to.Location,
				Next = null,
				StepsFromStart = 0,
				EstimatedRemainingPath = EstimatePathLengthSqr(to.Location, from.Location)
			};

			open.Add(start);

			while (open.Count > 0)
			{
				var current = open.OrderBy(n => n.EstimatedRemainingPath).First();
				if (current.Location == from.Location)
					return GetPathFor(current, field);
				open.Remove(current);
				closed.Add(current);

				var near = Neighbours(current, from.Location, field);
				foreach (var neighbour in near)
				{
					if (closed.Count(node => node.Location == neighbour.Location) > 0)
						continue;

					var openNode = open.FirstOrDefault(node => node.Location == neighbour.Location);

					if (openNode == null)
						open.Add(neighbour);
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
			var t = current.Next;

			while (t.Next != null)
			{
				result.Add(field[t.Location.X, t.Location.Y, t.Location.Z]);
				Console.WriteLine(Math.Sqrt(t.EstimatedRemainingPath));
				t = t.Next;
			}

			return result;
		}

		public static int EstimatePathLengthSqr(Vector from, Vector to)
			=> (from.X * from.X - to.X * to.X) + (from.Y * from.Y - to.Y * to.Y) + (from.Z * from.Z - to.Z * to.Z);

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
						EstimatedRemainingPath = EstimatePathLengthSqr(v, goal)
					};
		}

		internal class Node
		{
			public Vector Location { get; set; }
			public int StepsFromStart { get; set; }
			public Node Next { get; set; }
			public int EstimatedRemainingPath { get; set; }
			public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
		}
	}
}