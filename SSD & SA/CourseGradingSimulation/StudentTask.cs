namespace CourseGradingCalculator
{
	class StudentTask : Task
	{
		public StudentTask(string Name, Types taskType, double taskPrice)
			: base(Name, taskType, taskPrice) => this.Points = 0;

		public double Points { get; private set; }

		public double PercentagePoints => Points * 100 / Price;

		public void Complete(double FinalPoints)
			=> this.Points = FinalPoints >= 0 ? FinalPoints : 0;

		public void CompleteInPercent(double Percentage)
			=> this.Points = Percentage >= 0 ? this.Price * Percentage : 0;
	}
}