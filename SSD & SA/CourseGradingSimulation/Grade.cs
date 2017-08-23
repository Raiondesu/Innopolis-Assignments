namespace CourseGradingCalculator
{
	struct Grade
	{
		public Grade(string NewLiteral, double Min, double Max)
		{
			this.Literal = NewLiteral;
			this.Points = (Min, Max);
		}
		public string Literal;
		public (double Min, double Max) Points;

		public static Grade Empty(string Literal) => new Grade(Literal, 0, 0);

		public override string ToString() => this.Literal;
	}
}