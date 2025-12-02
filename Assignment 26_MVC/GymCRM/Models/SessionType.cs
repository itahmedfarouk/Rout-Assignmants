namespace GymCRM.Model
{
    public class SessionType
    {
        public int Id { get; set; }
        public string TitleAr { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public int DefaultDurationMin { get; set; } = 60;
        public bool IsActive { get; set; } = true;
    }
}
