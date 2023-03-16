using System;
namespace LinqMethods
{
    class Program
    {
        public class Student
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public int Age { get; set; }
            public string Major { get; set; }
            public double Tuition { get; set; }
        }

        public class StudentClubs
        {
            public int StudentID { get; set; }
            public string ClubName { get; set; }
        }

        public class StudentGPA
        {
            public int StudentID { get; set; }
            public double GPA { get; set; }
        }

        static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                    new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                    new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                    new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                    new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                    new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                    new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                    new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                    new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                    new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                    new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                    new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                    new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                    new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                    new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
                new StudentClubs() {StudentID=1, ClubName="Photography" },
                new StudentClubs() {StudentID=1, ClubName="Game" },
                new StudentClubs() {StudentID=2, ClubName="Game" },
                new StudentClubs() {StudentID=5, ClubName="Photography" },
                new StudentClubs() {StudentID=6, ClubName="Game" },
                new StudentClubs() {StudentID=7, ClubName="Photography" },
                new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            var gbGPA = studentGPAList.GroupBy(s => s.GPA);
            var sbClub = studentClubList.OrderBy(s => s.ClubName).GroupBy(s => s.ClubName);
            var cGPA = studentGPAList.Where(s => s.GPA >= 2.5 && s.GPA <= 4.0).Count();
            var aTuition = studentList.Average(s => s.Tuition);
            var htStudents = studentList.Where(s => s.Tuition == studentList.Max(s => s.Tuition)).ToList();
            var sglInnerJoin = studentList.Join(studentGPAList, student => student.StudentID, GPA => GPA.StudentID, (student, GPA) => new {StudentName = student.StudentName, Major = student.Age, GPA = GPA.GPA });
            var slcInnerJoin = studentList.Join(studentClubList, student => student.StudentID, club => club.StudentID, (student, club) => new {StudentName = student.StudentName, ClubName = club.ClubName}).Where(s => s.ClubName == "Game").ToList();

            Console.WriteLine("Grouped by GPA:\n");
            foreach (var student in gbGPA)
            {
                foreach (var s in student)
                {
                    Console.WriteLine($"Student: {s.StudentID}    GPA: {s.GPA}");
                }
            }
            Console.WriteLine("\nSorted & Grouped by club name:\n");
            foreach (var student in sbClub)
            {
                foreach (var s in student)
                {
                    Console.WriteLine($"Student: {s.StudentID}    GPA: {s.ClubName}");
                }
            }
            Console.WriteLine($"\nStudents with GPA between 2.5 and 4.0: {cGPA}");
            Console.WriteLine($"\nAverage Tuition: {String.Format("{0:C}", aTuition)}");
            Console.WriteLine($"\nStudents with the highest Tuition:\n");
            foreach (var student in htStudents)
            {
                Console.WriteLine($"Student: {student.StudentName}    Major: {student.Major}    Tuition: {student.Tuition}");
            }
            Console.WriteLine($"\nStudents Major & GPA:\n");
            foreach (var student in sglInnerJoin)
            {
                Console.WriteLine($"Student: {student.StudentName}    Major: {student.Major}    GPA: {student.GPA}");
            }
            Console.WriteLine($"\nStudents in the game club:\n");
            foreach (var student in slcInnerJoin)
            {
                Console.WriteLine($"Student: {student.StudentName}");
            }
        }
    }
}