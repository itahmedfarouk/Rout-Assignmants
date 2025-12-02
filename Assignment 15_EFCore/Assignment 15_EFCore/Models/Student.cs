namespace Assignment01_Ef_Core.Models
{

        public class Student
        {
            public int Id { get; set; }
            public string FName { get; set; } = string.Empty;
            public string LName { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public int Age { get; set; }
            public int Dep_Id { get; set; }
        }
    }

