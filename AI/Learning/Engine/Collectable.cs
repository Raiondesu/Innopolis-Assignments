namespace Learning.Engine
{
	public class Collectable : Actor, ICollectable
	{
		public Collectable(Vector2D initialLocation, int value, bool isFinal = false) : base(initialLocation)
		{
			this.Value = value;
			this.IsFinal = isFinal;
		}

		public int Value { get; }
		public bool IsFinal { get; }
	}
}