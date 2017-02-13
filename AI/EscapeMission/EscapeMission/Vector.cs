namespace EscapeMission
{
    public class Vector
    {
	    public Vector(int x = 0, int y = 0, int z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
	    public int Z { get; set; }

	    public static bool operator ==(Vector left, Vector right)
		    => left?.X == right?.X && left?.Y == right?.Y && left?.Z == right?.Z;

	    public static bool operator !=(Vector left, Vector right) => !(left == right);

	    public override bool Equals(object obj)
	    {
		    if (ReferenceEquals(null, obj)) return false;
		    if (ReferenceEquals(this, obj)) return true;
		    if (obj.GetType() != this.GetType()) return false;
		    return Equals((Vector) obj);
	    }

	    public override int GetHashCode()
	    {
		    unchecked
		    {
			    var hashCode = X;
			    hashCode = (hashCode * 397) ^ Y;
			    hashCode = (hashCode * 397) ^ Z;
			    return hashCode;
		    }
	    }

	    protected bool Equals(Vector other) => X == other.X && Y == other.Y && Z == other.Z;
    }
}