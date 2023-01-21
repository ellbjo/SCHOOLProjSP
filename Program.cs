using Microsoft.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace SCHOOLProj
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Repo repo = new Repo();
            Console.WriteLine("Welcome to the school information Application");
            string command = "";
            while (!command.ToLower().Equals("exit"))
            {

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Pick which command you want to run:");
                Console.WriteLine("AP = Add Personell");
                Console.WriteLine("VP = View Personell");
                Console.WriteLine("AS = Add Student");
                Console.WriteLine("VS = View Students");
                Console.WriteLine("GSBI = Get Student By ID");
                Console.WriteLine("AG = Add Grade");
                Console.WriteLine("PBD = Personell by department");
                Console.WriteLine("SAC = Show active courses");
                Console.WriteLine("SBD = Salary By Department");
                Console.WriteLine("ASBD = Avarage Salary By Department");
                Console.WriteLine("EXIT = Exit application");

                command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "ap":
                        repo.AddPersonell();
                        break;
                    case "vp":
                        repo.GetPersonell();
                        break;
                    case "as":
                        repo.AddStudent();
                        break;
                    case "vs":
                        repo.GetStudentsFromEF();
                        break;
                    case "gsbi":
                        Console.WriteLine("What student ID do you want to search on? Write a number and press Enter.");
                        int id = int.Parse(Console.ReadLine());
                        repo.GetStudentOnId(id);
                        break;
                    case "ag":
                        repo.SetGrade();
                        break;
                    case "pbd":
                        repo.GetAmountOfEmployeesForEachRole();
                        break;
                    case "sac":
                        repo.ShowActiveCourses();
                        break;
                    case "sbd":
                        repo.GetSalaryOnRoleForEachMonth();
                        break;
                    case "asbd":
                        repo.GetAvarageSalaryOnRoleForEachMonth();
                        break;
                    case "exit":
                        Console.WriteLine("Exit application, have a great day!");
                        break;
                    default:
                        break;
                }
                Console.WriteLine("----------------------------------------");

                

            }
        }
    }
}