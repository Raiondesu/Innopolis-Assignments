using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ParticleSwarm
{
	class Vector : IEnumerable<double>
	{
		public static readonly Vector MaxVector = new Vector(
			int.MaxValue,
			int.MaxValue,
			int.MaxValue,
			int.MaxValue,
			int.MaxValue
		);

		public static readonly Vector MinVector = new Vector(
			int.MinValue,
			int.MinValue,
			int.MinValue,
			int.MinValue,
			int.MinValue
		);

		private const int defaultSize = 5;
		protected double[] coordinates;

		public Vector(params double[] coords)
		{
			this.coordinates = coords;
		}

		public Vector(int size = defaultSize)
		{
			this.coordinates = new double[size];
			for (int i = 0; i < this.Size; i++)
				this[i] = 0;
		}

		public int Size => coordinates.Length;
		public double Length => Math.Sqrt(this.Fitness);
		public double Fitness => this.Sum(c => c * c);

		public static Vector operator +(Vector v1, Vector v2)
		{
			int largestSize = v1.Size > v2.Size ? v1.Size : v2.Size;
			double[] coords = new double[largestSize];
			double i1, i2;

			for (int i = largestSize; i --> 0;)
			{
				i1 = i < v1.Size ? v1[i] : 0;
				i2 = i < v2.Size ? v2[i] : 0;
				coords[i] = i1 + i2;
			}

			return new Vector(coords);
		}

		public static Vector operator -(Vector v1, Vector v2) => v1 + (~v2);

		public static Vector operator +(Vector v, double d) => v.ForEach((c, i) => c + d);
		public static Vector operator +(double d, Vector v) => v + d;
		public static Vector operator -(Vector v, double d) => v.ForEach((c, i) => c - d);
		public static Vector operator -(double d, Vector v) => v - d;
		public static Vector operator *(Vector v, double d) => v.ForEach((c, i) => c * d);
		public static Vector operator *(double d, Vector v) => v * d;

		public static Vector operator ~(Vector v)
		{
			Vector res = v;
			for (int i = 0; i < v.Size; i++)
				res[i] = -v[i];

			return res;
		}

		public double this[int index]
		{
			get	{ return this.coordinates[index]; }
			set
			{
				if (value >= Globals.Max)
					this.coordinates[index] = Globals.Max;
				else if (value <= Globals.Min)
					this.coordinates[index] = Globals.Min;
				else
					this.coordinates[index] = value;
			}
		}

		public static implicit operator double[](Vector v) => v.coordinates;
		public static implicit operator Vector(double[] d) => new Vector(d);

		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<double>)coordinates).GetEnumerator();
		public IEnumerator<double> GetEnumerator() => ((IEnumerable<double>)coordinates).GetEnumerator();
		public Vector ForEach(Func<double, int, double> func)
		{
			for (int i = 0; i < this.Size; i++)
				this[i] = func(this[i], i);

			return this;
		}

		public override string ToString()
		{
			string res = this[0].ToString();
			for (int i = 1; i < this.Size; i++)
				res += ", " + this[i];

			return $"[{res}]";
		}
	}
}
