using System;
using System.Numerics;

namespace Quoridor
{
	public class Vector2D : IFormattable
	{
		private Vector<int> v;

		public Vector2D() : this(0) {}
		public Vector2D(int c) : this(c, c) {}
		public Vector2D(int x, int y) => this.v = new Vector<int>(new [] {x, y});

		public int X { get => this.v[0]; set => this.v = new Vector2D(value, Y); }
		public int Y { get => this.v[1]; set => this.v = new Vector2D(X, value); }

		public Vector2D Clamp(Vector2D min, Vector2D max) => this < min ? min : this > max ? max : this;

		public static readonly Vector2D Zero = new Vector2D(0);
		public static readonly Vector2D Unit = new Vector2D(1);

		public static Vector2D operator -(Vector2D v) => -v.v;
		public static Vector2D operator ~(Vector2D v) => ~v.v;
		public static Vector2D operator +(Vector2D v1, Vector2D v2) => v1.v + v2.v;
		public static Vector2D operator -(Vector2D v1, Vector2D v2) => v1.v - v2.v;
		public static Vector2D operator *(Vector2D v1, Vector2D v2) => v1.v * v2.v;
		public static Vector2D operator /(Vector2D v1, Vector2D v2) => v1.v / v2.v;
		public static Vector2D operator &(Vector2D v1, Vector2D v2) => v1.v & v2.v;
		public static Vector2D operator |(Vector2D v1, Vector2D v2) => v1.v | v2.v;
		public static Vector2D operator ^(Vector2D v1, Vector2D v2) => v1.v ^ v2.v;

		public static bool operator <(Vector2D v1, Vector2D v2) => v1.X < v2.X && v1.Y < v2.Y;
		public static bool operator >(Vector2D v1, Vector2D v2) => v1.X > v2.X && v1.Y > v2.Y;
		public static bool operator <=(Vector2D v1, Vector2D v2) => v1 < v2 || v1 == v2;
		public static bool operator >=(Vector2D v1, Vector2D v2) => v1 > v2 || v1 == v2;
		public static bool operator ==(Vector2D v1, Vector2D v2) => v1.Equals(v2);
		public static bool operator !=(Vector2D v1, Vector2D v2) => !(v1 == v2);

		public static implicit operator Vector<int>(Vector2D v) => v.v;
		public static implicit operator Vector2D(int i) => new Vector2D(i);
		public static implicit operator Vector2D(Vector<int> v) => new Vector2D(v[0], v[1]);

		public override bool Equals(object other) => this.X == (other as Vector2D)?.X && this.Y == (other as Vector2D)?.Y;
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