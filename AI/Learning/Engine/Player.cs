using System;
using System.Collections.Generic;

namespace Learning.Engine
{
	public class Player : Pawn
	{
		private Random rand = new Random();
		private int LF = 1;
		private int DF = 1;
		private int[,] Q;
		private int[,] V;

		public Player(Vector2D initialLocation) : base(initialLocation) {}
		public Player(Player copy) : base(copy) {}

		public void PlayOn(ref Map map)
		{
			Q = new int[map.Size.X, map.Size.Y];
			
			var states = map.GetAllPossiblePlayerStates();

			for (int i = 0; i < Q.GetLength(0); i++)
				for (int j = 0; j < Q.GetLength(1); j++)
					Q[i, j] = 0;


			Map s;
			int r;
			Func<int> a = null;

			s = map.Copy();
			r = this.LastValue;
			// Q[this.Location.X, this.Location.Y] += LF * (r + DF * Max(Q, s) - Q[this.Location.X, this.Location.Y]);

			// a = ArgMax(Q, s);

			var activator = a;

		}

		public double QLearning(Vector2D loc, Vector2D prevLoc, int depth, Map map)
		{
			if (depth < 0) return Q[loc.X, loc.Y];

			var states = map.GetAllPossiblePlayerStates();
			double max = int.MinValue;
			var maxLoc = loc;
			bool isRandom = rand.Next(100) < 1;
			if (isRandom)
				states[rand.Next(states.Count)].a();
			else
			{
				
			}
		}
	}
}