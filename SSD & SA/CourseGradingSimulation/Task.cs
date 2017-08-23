namespace CourseGradingCalculator
{
	class Task
	{
		public Task(string Name, Types taskType, double taskPrice)
		{
			this.Name = Name;
			this.Price = taskPrice;
			this.Type = taskType;
		}

		public string Name { get; }
		public double Price { get; }
		public Types Type { get; }

		public enum Types
		{
			Assignment,
			Project,
			Exam,
			Quiz,
			Test
		}
	}
}