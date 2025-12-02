namespace GymCRM.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Gender Gender { get; set; }

        public string? UserId { get; set; }   // 👈 مهم لربط AspNetUsers
    }
}
