namespace Assignment01_Ef_Core.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Adress { get; set; } = string.Empty; // حسب السكيمة
        public decimal HourRateBouns { get; set; }
        public int Dept_ID { get; set; }
    }
}
