using System;
using System.Diagnostics;
using System.Linq;

namespace ParticleSwarm
{
    class Program
	{
		static void Main(string[] args)
		{
			Particle[] particles = new Particle[int.Parse(args[0])];
			Globals.Min = int.Parse(args[2]);
			Globals.Max = int.Parse(args[3]);
			Globals.DefSize = args.Length >= 5 ? int.Parse(args[4]) : 5;

			for (int i = particles.Length; i --> 0;)
				particles[i] = new Particle(Globals.randomVectorInBounds, new Vector(5));

			int maxIterations = int.Parse(args[1]);

			Stopwatch timer = Stopwatch.StartNew();
			
			do
			{
				foreach (var particle in particles)	if (particle.Position.Fitness < particle.Best.Fitness)
					particle.Best = particle.Position;

				Particle best = particles.OrderBy(p => p.Best.Fitness).First();

				if (Globals.Best == null || best.Best.Fitness < Globals.Best.Best.Fitness)
					Globals.Best = best;
				
				foreach (var particle in particles)	particle.Update();

				Console.WriteLine(Globals.Best.Best.Fitness);
			} while(maxIterations --> 0);

			timer.Stop();
			Console.WriteLine($"{Globals.Best.Best.Fitness} : {timer.ElapsedMilliseconds}ms");
		}
	}
}
