// Services/NotificationService.cs
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GymCRM.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymCRM.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationOptions _opts;
        private readonly ILogger<NotificationService> _logger;
        private readonly IHttpClientFactory? _httpClientFactory;

        public NotificationService(
            IOptions<NotificationOptions> options,
            ILogger<NotificationService> logger,
            IHttpClientFactory? httpClientFactory = null)
        {
            _opts = options.Value;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        // ===== EMAIL =====
        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody, string? plainTextBody = null, string? displayName = null)
        {
            if (!_opts.Email.Enabled)
            {
                _logger.LogInformation("Email disabled. Skipping email to {to}. Subject: {subj}", toEmail, subject);
                return;
            }

            using var msg = new MailMessage
            {
                From = new MailAddress(_opts.Email.From, displayName ?? _opts.Email.FromDisplayName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            msg.To.Add(new MailAddress(toEmail));

            if (!string.IsNullOrWhiteSpace(plainTextBody))
            {
                // إضافة نسخة نصية (اختياري)
                var plainView = AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain");
                msg.AlternateViews.Add(plainView);
            }

            using var smtp = new SmtpClient(_opts.Email.Host, _opts.Email.Port)
            {
                EnableSsl = _opts.Email.UseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            if (!string.IsNullOrWhiteSpace(_opts.Email.User))
                smtp.Credentials = new NetworkCredential(_opts.Email.User, _opts.Email.Password);

            await smtp.SendMailAsync(msg);
            _logger.LogInformation("Email sent to {to}", toEmail);
        }

        // ===== SMS (Twilio REST API كمثال بسيط) =====
        public async Task SendSmsAsync(string toPhone, string message)
        {
            if (!_opts.Sms.Enabled)
            {
                _logger.LogInformation("SMS disabled. Skipping SMS to {to}. Body: {msg}", toPhone, message);
                return;
            }

            if (!string.Equals(_opts.Sms.Provider, "Twilio", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Unsupported SMS provider: {provider}", _opts.Sms.Provider);
                return;
            }

            if (string.IsNullOrWhiteSpace(_opts.Sms.AccountSid) ||
                string.IsNullOrWhiteSpace(_opts.Sms.AuthToken) ||
                string.IsNullOrWhiteSpace(_opts.Sms.From))
            {
                _logger.LogError("Twilio options missing (AccountSid/AuthToken/From).");
                return;
            }

            var client = _httpClientFactory?.CreateClient() ?? new HttpClient();
            var url = $"https://api.twilio.com/2010-04-01/Accounts/{_opts.Sms.AccountSid}/Messages.json";

            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("To", toPhone),
                    new KeyValuePair<string,string>("From", _opts.Sms.From!),
                    new KeyValuePair<string,string>("Body", message),
                })
            };

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_opts.Sms.AccountSid}:{_opts.Sms.AuthToken}"));
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var res = await client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                _logger.LogError("Twilio SMS failed. Status: {status}. Body: {body}", res.StatusCode, body);
            }
            else
            {
                _logger.LogInformation("SMS sent to {to}", toPhone);
            }
        }

        // ===== Template إيصال دفع مختصر =====
        public async Task SendReceiptAsync(Customer customer, Subscription subscription)
        {
            var subject = $"إيصال الدفع — GymCRM (#{subscription.Id})";
            var html = $@"
                <div style='font-family:Tahoma,Arial,sans-serif'>
                  <h3>شكرًا لاشتراكك في GymCRM</h3>
                  <p>عزيزي/عزيزتي <b>{WebUtility.HtmlEncode(customer.FullName)}</b>,</p>
                  <p>تم استلام دفعتك بنجاح.</p>
                  <table style='border-collapse:collapse'>
                    <tr><td style='padding:4px'>رقم الاشتراك:</td><td style='padding:4px'><b>{subscription.Id}</b></td></tr>
                    <tr><td style='padding:4px'>الإجمالي:</td><td style='padding:4px'><b>{subscription.Total:N2} ر.س</b></td></tr>
                    <tr><td style='padding:4px'>الضريبة:</td><td style='padding:4px'>{subscription.VatAmount:N2} ر.س</td></tr>
                    <tr><td style='padding:4px'>مرجع الدفع:</td><td style='padding:4px'>{WebUtility.HtmlEncode(subscription.PaymentRef ?? "-")}</td></tr>
                    <tr><td style='padding:4px'>الحالة:</td><td style='padding:4px'><b>{WebUtility.HtmlEncode(subscription.Status)}</b></td></tr>
                  </table>
                  <p style='margin-top:14px'>نتمنى لك تجربة رياضية رائعة! 💪</p>
                </div>";

            await SendEmailAsync(customer.Email, subject, html);
            // SMS مختصر (اختياري)
            await SendSmsAsync(customer.Phone, $"GymCRM: تم دفع {subscription.Total:N2} ر.س، اشتراك #{subscription.Id}.");
        }
    }
}
