using System;

namespace Quoridor
{
	public struct Vector2D : IFormattable
	{
		public Vector2D(int c = 0) : this(c, c) {}
		public Vector2D(int x, int y) { this.X = x; this.Y = y; }

		public int X { get; set; }
		public int Y { get; set; }

		public bool FitsIn(Vector2D min, Vector2D max)
			=> this >= min && this <= max;

		public Vector2D RotateLeft() => (-this.Y, this.X);
		public Vector2D RotateRight() => (this.Y, -this.X);

		public static readonly Vector2D Zero = new Vector2D(0);
		public static readonly Vector2D Unit = new Vector2D(1);

		public static Vector2D operator -(Vector2D v) => new Vector2D(-v.X, -v.Y);
		public static Vector2D operator ~(Vector2D v) => new Vector2D(~v.X, ~v.Y);
		public static Vector2D operator +(Vector2D v1, Vector2D v2) => new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		public static Vector2D operator -(Vector2D v1, Vector2D v2) => new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		public static Vector2D operator *(Vector2D v1, Vector2D v2) => new Vector2D(v1.X * v2.X, v1.Y * v2.Y);
		public static Vector2D operator /(Vector2D v1, Vector2D v2) => new Vector2D(v1.X / v2.X, v1.Y / v2.Y);
		public static Vector2D operator &(Vector2D v1, Vector2D v2) => new Vector2D(v1.X & v2.X, v1.Y & v2.Y);
		public static Vector2D operator |(Vector2D v1, Vector2D v2) => new Vector2D(v1.X | v2.X, v1.Y | v2.Y);
		public static Vector2D operator ^(Vector2D v1, Vector2D v2) => new Vector2D(v1.X ^ v2.X, v1.Y ^ v2.Y);

		public static bool operator <(Vector2D v1, Vector2D v2) => v1.X < v2.X && v1.Y < v2.Y;
		public static bool operator >(Vector2D v1, Vector2D v2) => v1.X > v2.X && v1.Y > v2.Y;
		public static bool operator <=(Vector2D v1, Vector2D v2) => v1.X <= v2.X && v1.Y <= v2.Y;
		public static bool operator >=(Vector2D v1, Vector2D v2) => v1.X >= v2.X && v1.Y >= v2.Y;
		public static bool operator ==(Vector2D v1, Vector2D v2) => v1.Equals(v2);
		public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

		public static implicit operator Vector2D(int i) => new Vector2D(i);
		public static implicit operator Vector2D((int X, int Y) v) => new Vector2D(v.X, v.Y);
		public static implicit operator (int X, int Y)(Vector2D v) => (v.X, v.Y);

		public override bool Equals(object other) => this.X == ((Vector2D)other).X && this.Y == ((Vector2D)other).Y;
		public string ToString(string format, IFormatProvider formatProvider) => $"[{X}, {Y}]";
		public override int GetHashCode()
		{
			unchecked
			{
				var result = 0;
				result = (result * 397) ^ this.X;
				result = (result * 397) ^ this.Y;
				return result;
			}
		}
	}
}