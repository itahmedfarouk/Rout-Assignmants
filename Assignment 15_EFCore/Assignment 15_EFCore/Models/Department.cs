using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment01_Ef_Core.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("Ins_ID")]
        public int InstructorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }
    }
}
