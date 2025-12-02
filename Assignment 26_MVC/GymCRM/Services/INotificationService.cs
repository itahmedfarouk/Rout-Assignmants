// Services/INotificationService.cs
using System.Threading.Tasks;
using GymCRM.Model;

namespace GymCRM.Services
{
    // إعدادات الإشعارات (تُقرأ من appsettings.json)
    public class NotificationOptions
    {
        public EmailOptions Email { get; set; } = new();
        public SmsOptions Sms { get; set; } = new();
    }

    public class EmailOptions
    {
        public bool Enabled { get; set; } = false;
        public string From { get; set; } = "";           // example@yourdomain.com
        public string? FromDisplayName { get; set; } = "GymCRM";
        public string Host { get; set; } = "";           // smtp.server.com
        public int Port { get; set; } = 587;
        public string? User { get; set; }
        public string? Password { get; set; }
        public bool UseSsl { get; set; } = true;
    }

    public class SmsOptions
    {
        public bool Enabled { get; set; } = false;
        public string Provider { get; set; } = "Twilio"; // أو أي مزوّد
        public string? AccountSid { get; set; }
        public string? AuthToken { get; set; }
        public string? From { get; set; }                // رقم المرسل
    }

    public interface INotificationService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody, string? plainTextBody = null, string? displayName = null);
        Task SendSmsAsync(string toPhone, string message);

        // طريقة مختصرة لرسالة إيصال بعد الدفع
        Task SendReceiptAsync(Customer customer, Subscription subscription);
    }
}
