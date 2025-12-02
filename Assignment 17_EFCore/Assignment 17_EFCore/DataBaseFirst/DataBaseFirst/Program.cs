using DataBaseFirst.Models;

namespace DataBaseFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITIContext context = new ITIContext();
            //var res =  context.Procedures.GetStudentsPerDepartmentAsync().Result;

            // Same as

            ITIContextProcedures contextProcedures = new ITIContextProcedures(context);
            var res = contextProcedures.GetStudentsPerDepartmentAsync().Result;
            foreach (var item in res)
            {
                Console.WriteLine(res);
            }
        }
    }
}



// Scaffold-DbContext -Connection "Server=AHMED-HISHAM ; Database=ITI ; Trusted_Connection=True; TrustServerCertificate=True" -Provider "Microsoft.EntityFrameWorkCore.SqlServer" -Context "ITIDbContext" -ContextDir "Data" -OutputDir "Data/Models"