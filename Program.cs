using Microsoft.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace SCHOOLProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source = DESKTOP-B2K268C; Initial Catalog = SCHOOL; Integrated Security = true");

            Repo repo1 = new Repo();
            //repo1.GetSalaryOnRoleForEachMonth();
            //repo1.GetAvarageSalaryOnRoleForEachMonth();
            //repo1.AddPersonell();
            //repo1.GetAmountOfEmployeesForEachRole();
            repo1.SetGrade();
            repo1.GetStudentOnId(3);

        }
    }
}