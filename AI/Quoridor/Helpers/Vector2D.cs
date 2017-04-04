using System;

namespace Quoridor
{
	public class Vector2D : IFormattable
	{
		public Vector2D() : this(0) {}
		public Vector2D(int c) : this(c, c) {}
		public Vector2D(int x, int y) { this.X = x; this.Y = y; }

		public int X { get; set; }
		public int Y { get; set; }

		public bool FitsIn(Vector2D bounds)
			=> this.X > 0 && this.Y > 0 && this < bounds;

		public Vector2D Clamp(Vector2D min, Vector2D max)
			=> (this.X < min.X ? min.X : this.X > max.X ? max.X : this.X, this.Y < min.Y ? min.Y : this.Y > max.Y ? max.Y : this.Y);

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

		public override bool Equals(object other) => this?.X == (other as Vector2D)?.X && this?.Y == (other as Vector2D)?.Y;
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