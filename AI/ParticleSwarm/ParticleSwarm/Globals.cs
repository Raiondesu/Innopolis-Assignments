using System;

namespace ParticleSwarm
{
    static class Globals
    {
        public static Particle Best = null;
		public static double GlobalAccCoefficient = 0.2;
		public static double PersonalAccCoefficient = 0.2;
		public static double M = 0.5;
		public static int Min = -5;
		public static int Max = 5;
		public static int DefSize = 5;
		public static Random rand = new Random();
		public static Vector randomVectorInBounds
		{
			get
			{
				double[] res = new double[DefSize];
				for (int i = 0; i < DefSize; i++)
					res[i] = rand.Next(Min, Max);
				return new Vector(res);
			}
		}
    }
}
