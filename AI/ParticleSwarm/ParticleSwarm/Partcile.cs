namespace ParticleSwarm
{
	class Particle
	{
		public Particle(Vector pos, Vector vel)
		{
			this.Position = pos;
			this.Velocity = vel;
		}

		public Vector Position { get; private set; }
		public Vector Velocity { get; private set; }
		public Vector Best { get; set; } = Vector.MaxVector;
		public double Coefficient { get; } = Globals.PersonalAccCoefficient;

		private Vector NewVelocity()
			=> (Globals.M * Velocity)
				+ (this.Coefficient * Globals.rand.NextDouble() * (this.Best - this.Position))
				+ (Globals.GlobalAccCoefficient * Globals.rand.NextDouble() * (Globals.Best.Best - this.Position));

		public void Update()
		{
			this.Velocity = this.NewVelocity();
			this.Position += this.Velocity;
		}
	}
}
