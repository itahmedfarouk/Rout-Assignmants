using Assignment01_Ef_Core;

using (var context = new AppDbContext())
{
    context.Database.EnsureCreated();
    Console.WriteLine("Database Created Successfully!");
}
