using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment01_Ef_Core.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column("Top_ID")]
        public int Top_Id { get; set; }
    }
}
