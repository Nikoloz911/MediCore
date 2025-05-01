using System.Net;
using System.Net.Mail;

namespace MediCore.SMTP
{
    public class SMTP_Visit_Reminder
    {
        public void SendReminderEmail(string toAddress, string subject, string body)
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


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = htmlContent;
            mail.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword)
            };
            smtpClient.Send(mail);
        }
    }
}
