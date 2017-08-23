using System;

namespace CourseGradingCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Course SA = new Course(
                "Software Architecture",
                new [] { 
                    ("D", 55.0),
                    ("C", 70.0),
                    ("B", 85.0),
                    ("A", 100.0)
                },
                new Task("Assignment 1", Task.Types.Assignment, 10),
                new Task("Assignment 2", Task.Types.Assignment, 10),
                new Task("Assignment 3", Task.Types.Assignment, 10),
                new Task("Project", Task.Types.Project, 50),
                new Task("Midterm Exam", Task.Types.Exam, 20),
                new Task("Final Exam", Task.Types.Exam, 20)
            );

            var finalpoints = 85;

            Console.WriteLine("A grading scheme on " + SA.Name + " course:");

            foreach (var grade in SA.Grades)
                Console.WriteLine($"{grade.Literal} is ({grade.Points.Min}, {grade.Points.Max}]");

            Console.WriteLine();
            Console.WriteLine($"You've got {SA.GetCourseGradeByPoints(finalpoints)} with {finalpoints}.");
            Console.WriteLine();

            Console.Write("Alex");
            var Alex = new Student("Alex");

            SA.EnrollStudents(Alex);

            Console.WriteLine(" completed his project veery good..\n");
            Alex.Courses[SA]["Project"].CompleteInPercent(0.95);

            foreach (var course in Alex.Courses)
                foreach (var task in course.Value)
                {
                    Console.WriteLine(task.Key + ": " + task.Value.Points + "/" + task.Value.Price);
                }
            Console.WriteLine("\nBut his course grade is not this good overall:");

            var agrade = SA.GetGradeForStudent(Alex);
            Console.WriteLine(agrade.Literal + " with " + agrade.Points.Max + " points out of " + SA.SumPoints + " of " + SA.MaxPoints + " total.");
        }
    }
}
