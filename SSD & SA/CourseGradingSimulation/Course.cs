using System;
using System.Collections.Generic;
using System.Linq;
using GradeMap = System.Collections.Generic.Dictionary<string, (double Min, double Max)>;
using TaskMap = System.Collections.Generic.Dictionary<string, CourseGradingCalculator.Task>;

namespace CourseGradingCalculator
{
	class Course
	{
		private GradeMap grades = new GradeMap();
		private TaskMap courseTasks;
		private List<Student> students;

		public Course(string CourseName, (string Literal, double MaxPoints)[] Grades, params Task[] Tasks)
		{
			for (int i = 0; i < Grades.Length; i++)
			{
				var grade = Grades[i].Literal;
				var maxPoints = ((i == 0 ? 0 : Grades[i - 1].MaxPoints), Grades[i].MaxPoints);
				this.SetCourseGrade(grade, maxPoints);
			}

			this.Name = CourseName;
			
			this.courseTasks = new Dictionary<string, Task>(
				Tasks.ToList().ConvertAll(t => new KeyValuePair<string, Task>(t.Name, t))
			);

			this.MaxPoints = Grades.Max(g => g.MaxPoints);
			this.SumPoints = Tasks.Sum(t => t.Price);
			this.students = new List<Student>();
		}

		public Course(Course other)
		{
			this.Name = other.Name;
			this.MaxPoints = other.MaxPoints;
			this.grades = new GradeMap(other.grades);
			this.students = new List<Student>(other.students);
			this.courseTasks = new TaskMap(other.courseTasks);
		}

		public string Name { get; }
		public double MaxPoints { get; }
		public double SumPoints { get; }

		public int GradesAmount => grades.Count;
		public IEnumerable<Student> Students => this.students;
		public IEnumerable<(string Name, Task Task)> CourseTasks => this.courseTasks.Select(t => (t.Key, t.Value));
		public IEnumerable<Grade> Grades => this.grades.Select(pair => new Grade(pair.Key, pair.Value.Min, pair.Value.Max));

		public Grade GetCourseGradeByLiteral(string GradeLiteral)
		{
			if (this.grades.ContainsKey(GradeLiteral))
				return new Grade(GradeLiteral, this.grades[GradeLiteral].Min, this.grades[GradeLiteral].Max);
			else
				return Grade.Empty("NoSuchGrade");
		}

		public Grade GetCourseGradeByPoints(double Points)
		{
			string outbounded = "No such grade";

			if (Points <= 0)
				return Grade.Empty(this.grades.First(pair => pair.Value.Min == 0).Key);

			if (Points > this.MaxPoints)
				outbounded = grades.First(pair => pair.Value.Max == this.MaxPoints).Key;

			foreach (var grade in this.grades)
			{
				if (grade.Value.Min < Points && Points <= grade.Value.Max)
					return new Grade(grade.Key, Points, Points);
			}

			return Grade.Empty(outbounded);
		}

		public Grade GetGradeForStudent(Student Student)
		{
			if (!this.students.Contains(Student)) return Grade.Empty("Not enrolled");

			double resultingPoints = 0;

			foreach (var task in Student.Courses[this])
				resultingPoints += task.Value.Points;

			return this.GetCourseGradeByPoints(resultingPoints);
		}

		public void SetCourseGrade(string Grade, (double Min, double Max) Points)
		{
			if (this.grades.ContainsKey(Grade))
				this.grades[Grade] = Points;
			else
				this.grades.Add(Grade, Points);
		}

		public void RemoveGrade(string Grade)
		{
			if (this.grades.ContainsKey(Grade))
				this.grades.Remove(Grade);
		}

		public void EnrollStudents(params Student[] Students)
		{
			this.students.AddRange(Students);

			foreach (var stdnt in Students)
				stdnt.Courses.Add(
					this,
					this.courseTasks.ToDictionary(
						k => k.Key,
						t => new StudentTask(t.Value.Name, t.Value.Type, t.Value.Price)
					)
				);
		}
	}
}