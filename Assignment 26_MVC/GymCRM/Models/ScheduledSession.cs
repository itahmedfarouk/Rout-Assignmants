using System;

namespace GymCRM.Model
{
    public class ScheduledSession
    {
        public int Id { get; set; }
        public int SessionTypeId { get; set; }
        public SessionType SessionType { get; set; } = null!;
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public DateTime StartAtUtc { get; set; }
        public int DurationMin { get; set; }
        public int Capacity { get; set; } = 10;
        public bool IsCanceled { get; set; } = false;
    }
}
