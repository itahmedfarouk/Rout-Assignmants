using System;

namespace GymCRM.Model
{
    public class SessionBooking
    {
        public int Id { get; set; }

        public int ScheduledSessionId { get; set; }
        public ScheduledSession ScheduledSession { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public BookingStatus Status { get; set; } = BookingStatus.Booked;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? CancelRequestedAtUtc { get; set; }
        public string? AdminDecisionByUserId { get; set; }   // ApplicationUser.Id
        public DateTime? AdminDecisionAtUtc { get; set; }

        // ✅ حقول الحضور (لزر "تأكيد الحضور")
        // null = لم يُحدَّد بعد، true = حضر، false = لم يحضر (no-show)
        public bool? Attended { get; set; }
        public DateTime? AttendedAtUtc { get; set; }
    }
}
