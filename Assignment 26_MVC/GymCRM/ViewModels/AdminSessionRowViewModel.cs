namespace GymCRM.ViewModels
{
    public class AdminSessionRowViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Branch { get; set; } = "";
        public System.DateTime StartAtUtc { get; set; }
        public int DurationMin { get; set; }
        public int Capacity { get; set; }
        public bool IsCanceled { get; set; }
    }
}
