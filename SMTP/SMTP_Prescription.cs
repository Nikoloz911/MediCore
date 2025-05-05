using System.Net;
using System.Net.Mail;

namespace MediCore.SMTP
{
    public static class SMTP_Prescription
    {
        public static void SendPrescriptionEmailWithAttachment(string toAddress, string subject, string body, string attachmentPath)
        {
            string senderEmail = "nikalobjanidze014@gmail.com";
            string appPassword = "fmni efhs vbho uurv"; // APP PASSWORD

            string htmlContent = $@"
            <html>
            <body>
                <h2>{subject}</h2>
                <p>{body}</p>
            </body>
            </html>";

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };
            mail.To.Add(toAddress);

            if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
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
