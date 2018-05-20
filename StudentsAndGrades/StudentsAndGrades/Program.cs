using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAndGrades
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the total number of students: ");

            bool isCountInt = int.TryParse(Console.ReadLine(), out int countOfStudents);

            if (!isCountInt)
            {
                //For more complex solutions use a trycatch
                Console.WriteLine("Sorry, invalid argument provided");
                throw new InvalidOperationException("Invalid integer argument provided");
            }

            var students = CaptureStudentGrades(countOfStudents);

            var output = GenerateOutputFromStudentGrades(students);

            output.ForEach(line => { Console.WriteLine(line); });

            Console.ReadLine();
        }

        private static List<string> GenerateOutputFromStudentGrades(IEnumerable<Student> students)
        {
            List<string> result = new List<string>();
            double overallAverage = 0;

            foreach (var student in students)
            {
                var studentGradeAverage = student.RetrunAverageGrade_ToDouble;
                var studentOutput = $"StudentId: {student.Id} | Average: {studentGradeAverage} | Max: {student.Grades.Max()} | Min: {student.Grades.Min()} | Grade: {student.ReturnGradeAverage_ToChar()}";
                result.Add(studentOutput);
                overallAverage += studentGradeAverage;
            }

            result.Add($"Overall class average: {overallAverage / students.Count()}");

            return result;
        }

        private static IEnumerable<Student> CaptureStudentGrades(int countOfStudents = 1)
        {
            var result = new List<Student>();
            var requiredNumberOfGrades = 5;

            for (int i = 0; i < countOfStudents; i++)
            {
                var newStudent = new Student() { Id = i, Grades = new List<double>() };

                Console.WriteLine($"Enter {requiredNumberOfGrades} grades for student {i + 1}: ");

                while (newStudent.Grades.Count() < requiredNumberOfGrades)
                {
                    var isValidGrade = double.TryParse(Console.ReadLine(), out double grade);

                    if (isValidGrade)
                        newStudent.Grades.Add(grade);
                    else
                        Console.WriteLine("Invalid grade entered, please re-enter a valid grade");
                }

                for (int j = 0; j < requiredNumberOfGrades; j++)
                {
                    
                }

                result.Add(newStudent);
            }

            return result;
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public List<double> Grades { get; set; }

        internal double RetrunAverageGrade_ToDouble { get => Grades.Average(); }

        internal char ReturnGradeAverage_ToChar()
        {
            if (RetrunAverageGrade_ToDouble >= 90.0) { return 'A'; } else
            if (RetrunAverageGrade_ToDouble >= 80.0) { return 'B'; } else
            if (RetrunAverageGrade_ToDouble >= 70.0) { return 'C'; } else
            if (RetrunAverageGrade_ToDouble >= 60.0) { return 'D'; } else
            return 'F';
        }
    }
}