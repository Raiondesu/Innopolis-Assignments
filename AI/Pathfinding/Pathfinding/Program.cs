using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pathfinding
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			int[,,] theField = {
				{{0}, {0}, {0}, {0}, {0}},
				{{0}, {-1}, {0}, {-1}, {0}},
				{{0}, {0}, {0}, {0}, {0}},
				{{0}, {-1}, {-1}, {-1}, {0}},
				{{0}, {0}, {0}, {0}, {0}}
			};

			var path = AStar.FindPath(new Vector(0, 0, 0), new Vector(3, 4, 0), theField);

			foreach (var vector in path) Console.WriteLine(vector);
		}
	}

	class AStar
	{
		public static List<Vector> FindPath(Vector from, Vector to, int[,,] field)
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

				var near = Neighbours(current, to, field);
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
			return null;
		}

		private static List<Vector> GetPathFor(Node current)
		{
			var result = new List<Vector>();
			var temp = current;

			while (temp != null)
			{
				result.Add(temp.Position);
				temp = temp.Previous;
			}
			result.Reverse();

			return result;
		}

		private static int EstimatePathLength(Vector from, Vector to)
			=> Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y) + Math.Abs(from.Z - to.Z);

		private static List<Node> Neighbours(Node node, Vector goal, int[,,] field)
		{
			var result = new List<Node>();

			int x = node.Position.X;
			int y = node.Position.Y;
			int z = node.Position.Z;

			var near = new List<Vector>();

			near.AddRange(new [] {
				new Vector(x + 1, y, z),
				new Vector(x - 1, y, z),
				new Vector(x, y + 1, z),
				new Vector(x, y - 1, z),
				new Vector(x, y, z + 1),
				new Vector(x, y, z - 1)
			});

			foreach (var v in near)
			{
				if (v.X + 1 >= field.GetLength(0) || v.X - 1 > 0
					|| v.Y + 1 >= field.GetLength(1) || v.Y - 1 > 0
					|| v.Z + 1 >= field.GetLength(2) || v.Z - 1 > 0
				    || field[v.X, v.Y, v.Z] >= 0
				) near.Remove(v);

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
		public Vector Position { get; set; }
		public int StepsFromStart { get; set; }
		public Node Previous { get; set; }
		public int EstimatedRemainingPath { get; set; }
		public int FullPathLength => StepsFromStart + EstimatedRemainingPath;
	}

	public class Vector
	{
		public Vector(int x = 0, int y = 0, int z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public static bool operator ==(Vector left, Vector right)
			=> left?.X == right?.X && left?.Y == right?.Y && left?.Z == right?.Z;

		public static bool operator !=(Vector left, Vector right) => !(left == right);

		protected bool Equals(Vector other) => X == other.X && Y == other.Y && Z == other.Z;

		public override string ToString() => $"{X} {Y} {Z}";
	}
}