namespace Learning.Engine
{
	public abstract class Actor
	{
		public Actor(Vector2D initialLocation)
			=> this.Location = initialLocation;
			
		public Actor(Actor copy)
			=> this.Location = copy.Location;
			
		public Vector2D Location { get; protected set; }

		public override string ToString() => this.Location.ToString();
	}
}