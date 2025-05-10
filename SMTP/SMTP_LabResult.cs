using System.Net;
using System.Net.Mail;

namespace MediCore.SMTP
{
    public class SMTP_LabResult
    {
        public static void SendLabResultEmailWithAttachment(string toAddress, string subject, string body, string attachmentPath)
        {
            string senderEmail = "nikalobjanidze014@gmail.com";
            string appPassword = "";   // APP PASSWORD

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = $"<html><body><p>{body}</p></body></html>",
                IsBodyHtml = true
            };

            mail.To.Add(toAddress);
            if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
            {
                mail.Attachments.Add(new Attachment(attachmentPath));
            }
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };

            smtpClient.Send(mail);
        }
    }
}
