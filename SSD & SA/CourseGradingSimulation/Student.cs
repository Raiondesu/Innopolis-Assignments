using System.Collections.Generic;

namespace CourseGradingCalculator
{
	class Student
	{
		public Student(string Name) => this.Name = Name;

		public string Name { get; }
		public Dictionary<Course, Dictionary<string, StudentTask>> Courses { get; }
			= new Dictionary<Course, Dictionary<string, StudentTask>>();
	}
}