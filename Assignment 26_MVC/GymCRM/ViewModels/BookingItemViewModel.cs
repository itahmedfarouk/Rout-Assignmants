using System;
using GymCRM.Model;

namespace GymCRM.ViewModels
{
    public class BookingItemViewModel
    {
        public int BookingId { get; set; }

        public string SessionTitle { get; set; } = "";
        public string BranchName { get; set; } = "";

        public DateTime StartAtLocal { get; set; }
        public int DurationMin { get; set; }

        public BookingStatus Status { get; set; }

        // كان موجود عندك بالفعل
        public bool CanCancel { get; set; }

        // ✅ جديد: لزر "تأكيد الحضور"
        public bool CanCheckIn { get; set; }

        // ✅ جديد: حالة الحضور (null = لم يحدد بعد، true حضر، false لم يحضر)
        public bool? Attended { get; set; }
    }
}
