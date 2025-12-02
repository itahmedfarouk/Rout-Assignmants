using System.Net;
using System.Net.Mail;

namespace Demo.PL.Utilities
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("ahmedmedo90477@gmail.com", "vkaa tvkg pgws nuin");
                    client.Send("ahmedmedo90477@gmail.com", email.To, email.Subject, email.Body);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
