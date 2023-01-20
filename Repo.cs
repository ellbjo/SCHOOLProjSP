using Microsoft.Data.SqlClient;
using SCHOOLProj.Data;
using SCHOOLProj.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCHOOLProj
{
    internal class Repo
    {
        SqlConnection sqlcon = new SqlConnection(@"Data Source = DESKTOP-B2K268C; Initial Catalog = SCHOOL; Integrated Security = true");

        public Repo()
        {
            sqlcon.Open();
        }

        public void GetPersonell()
        {
            Console.WriteLine("-----------------------------------");

            string testCommand = "SELECT Personell.PersonellFName, Personell.PersonellLName, Personell.PersonellRole, Personell.Startdate FROM Personell";
            SqlDataAdapter data = new SqlDataAdapter(testCommand, sqlcon);
            DataTable t1 = new DataTable();
            data.Fill(t1);

            foreach (DataRow dr in t1.Rows)
            {
                Console.WriteLine(dr["PersonellFName"] + " " + dr["PersonellLName"] + " " + dr["PersonellRole"] + " " + dr["Startdate"]);
            }
        }
        public void AddStudent()
        {
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Firstname: ");
            var studentFName = Console.ReadLine();
            Console.WriteLine("Lastname:");
            var studentLName = Console.ReadLine();
            Console.WriteLine("Date of Birth (YYYY-MM-DD):");
            var dateOfBirth = Console.ReadLine();
            Console.WriteLine("Class:");
            var _class = Console.ReadLine();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO Students (StudentFName, StudentLName, DOB, Class) VALUES ('" + studentFName + "' , '" + studentLName + "' , '" + dateOfBirth + "' , '" + _class + "')";
            cmd.Connection = sqlcon;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Student " + studentFName + " has been added to the register to class " + _class);
        }

        public void AddPersonell()
        {
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Firstname: ");
            var personellFName = Console.ReadLine();
            Console.WriteLine("Lastname:");
            var personellLName = Console.ReadLine();
            Console.WriteLine("StartDate (YYYY-MM-DD):");
            var startDate = Console.ReadLine();
            Console.WriteLine("Role:");
            var role = Console.ReadLine();
            Console.WriteLine("Salary:");
            var salary = Console.ReadLine();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO Personell (PersonellRole, PersonellFName, PersonellLName, Startdate, Salary) VALUES ('" + role + "' , '" + personellFName + "' , '" + personellLName + "' , '" + startDate + "' , ' " + salary + "')";
            cmd.Connection = sqlcon;
            cmd.ExecuteNonQuery();
            Console.WriteLine("Personell " + personellLName + " has been added to the register to as " + role);
        }
        public void GetStudents()
        {
            Console.WriteLine("-----------------------------------");

            string getStudentCommand = "SELECT Students.StudentId, Students.StudentFName, Students.StudentLName FROM Students";
            SqlDataAdapter data = new SqlDataAdapter(getStudentCommand, sqlcon);
            DataTable t2 = new DataTable();
            data.Fill(t2);

            foreach (DataRow dr1 in t2.Rows)
            {
                Console.WriteLine(dr1["StudentId"] + " " + dr1["StudentFName"] + " " + dr1["StudentLName"]);
            }
        }

        public void GetStudentOnId(int id)
        {
            Console.WriteLine("-----------------------------------");

            string getStudentCommand = "SELECT Students.StudentId, Students.StudentFName, Students.StudentLName, Students.DOB, Students.Class FROM Students where Students.StudentId = '" + id + "';";
            SqlDataAdapter data = new SqlDataAdapter(getStudentCommand, sqlcon);
            DataTable t2 = new DataTable();
            data.Fill(t2);

            foreach (DataRow dr1 in t2.Rows)
            {
                Console.WriteLine($"ID: {dr1["StudentId"]}, Name: {dr1["StudentFName"]} {dr1["StudentLName"]}, Date of Birth: {dr1["DOB"]}, Class: {dr1["Class"]}");
            }
        }

        public void ShowCourses()
        {
            Console.WriteLine("-----------------------------------");



        }

        public void SetGrade()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Student Id:");
            var studentId = Console.ReadLine();
            Console.WriteLine("Course Id:");
            var courseId = Console.ReadLine();
            Console.WriteLine("Set grade:");
            var studentGrade = Console.ReadLine();
            Console.WriteLine("Date:");
            var setDate = Console.ReadLine();
            SqlTransaction trans = null;

            string getSC = "SELECT * FROM StudentCourses WHERE StudentCourses.FK_StudentId = '" + studentId + "' AND StudentCourses.FK_CourseId = '" + courseId + "' ";

            SqlDataAdapter data = new SqlDataAdapter(getSC, sqlcon);
            DataTable t3 = new DataTable();
            data.Fill(t3);
            string studentCourse = t3.Rows[0]["StId"].ToString();

            using (SqlConnection objConn = new SqlConnection(@"Data Source = DESKTOP-B2K268C; Initial Catalog = SCHOOL; Integrated Security = true"))
            {
                objConn.Open();
                trans = objConn.BeginTransaction();
                SqlCommand objCmd1 = new SqlCommand("INSERT INTO Grades (Grade, SetDate, FK_StudentCourse) VALUES ('" + studentGrade + "', '" + setDate + "' , '" + studentCourse + "')", objConn);

                try
                {
                    

                    //string setGrade = "INSERT INTO Grades (Grade, SetDate, FK_StudentCourse) VALUES ('" + studentGrade + "', '" + setDate + "' , '" + studentCourse + "')";
                    //SqlDataAdapter data2 = new SqlDataAdapter(setGrade, objConn);

                    //DataTable t4 = new DataTable();
                    //data2.Fill(t4);
                    objCmd1.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    trans.Rollback();
                }
                finally
                {
                    objConn.Close();
                }


            }
            //foreach (DataRow dr1 in t3.Rows)
            //{
            //    Console.WriteLine(dr1["StudentId"] + " " + dr1["StudentFName"] + " " + dr1["StudentLName"]);
            //}
        }

        public void ShowActiveCourses()
        {
            using (var context = new SCHOOLContext())
            {
                var activeCourses = from c in context.Courses
                                    where c.EndDate >= DateTime.Today
                                    select c;
                foreach (var c in activeCourses)
                {
                    Console.WriteLine(c.Subject);
                }
            }
        }

        public void GetSalaryOnRoleForEachMonth()
        {
            string query = "SELECT PersonellRole, SUM(Salary) as 'Total' from Personell GROUP BY PersonellRole";
            SqlDataAdapter data = new SqlDataAdapter(query, sqlcon);

            DataTable dt = new DataTable();
            data.Fill(dt);

            Console.WriteLine("Total salary for each role per month is: ");
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Role: " + dr["PersonellRole"] + " Cost per Month: " + dr["Total"]);
            }
        }

        public void GetAvarageSalaryOnRoleForEachMonth()
        {
            string query = "SELECT PersonellRole, AVG(Salary) as 'Avarage' from Personell GROUP BY PersonellRole";
            SqlDataAdapter data = new SqlDataAdapter(query, sqlcon);

            DataTable dt = new DataTable();
            data.Fill(dt);

            Console.WriteLine("Avarage salary for each role per month is: ");
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Role: " + dr["PersonellRole"] + " Cost per Month: " + dr["Avarage"]);
            }
        }

        public void GetAmountOfEmployeesForEachRole()
        {
            string query = "SELECT PersonellRole, COUNT(Salary) as 'Amount' from Personell GROUP BY PersonellRole";
            SqlDataAdapter data = new SqlDataAdapter(query, sqlcon);

            DataTable dt = new DataTable();
            data.Fill(dt);

            Console.WriteLine("Total salary for each role per month is: ");
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Role: " + dr["PersonellRole"] + " Amount employed: " + dr["Amount"]);
            }
        }

        public void GetStudentInformationFromId()
        {
            using (var context = new SCHOOLContext())
            {
                var students = from s in context.Students
                               select s
                                    ;

                //var student = students.Where(s => s.StudentId == id).FirstOrDefault<Student>();
                //Console.WriteLine($"Student: {student.StudentFname} {student.StudentLname} ");
                foreach (var s in students)
                {
                    Console.WriteLine($"Student: {s.StudentFname} {s.StudentLname}");
                    Console.WriteLine("Has these active courses:");

                }
            }

        }
    }
}
