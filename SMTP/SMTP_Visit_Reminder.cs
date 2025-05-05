using MediCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace MediCore.Services.Implementations
{
    public class SMTP_Visit_Reminder
    {
        private readonly DataContext _context;
        public SMTP_Visit_Reminder(DataContext context)
        {
            _context = context;
        }

        public static void SendReminderEmail(string toAddress, string subject, string body)
        {
            string senderEmail = "nikalobjanidze014@gmail.com";
            string appPassword = "fmni efhs vbho uurv"; // Your app password here

            var htmlContent = $@"
                <html>
                <body style='font-family:Segoe UI, sans-serif; background-color:#f0f8ff; padding:20px; color:#1a3e5d;'>
                    <div style='max-width:600px; margin:auto; background:white; padding:30px; border-radius:10px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>
                        <h2 style='color:#007BFF; margin-bottom:20px;'>MediCore Medical System</h2>
                        <p>Dear <strong>{subject}</strong>,</p>
                        <p>{body}</p>
                        <footer style='font-size:12px; color:#777;'>This is an automated reminder. Please do not reply to this email.</footer>
                    </div>
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

        public void SendAppointmentReminders()
        {
            var tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

            var appointments = _context.Appointments
                .Where(a => a.Date == tomorrow)
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .ToList();

            foreach (var appointment in appointments)
            {
                var user = appointment.Patient?.User;
                if (user == null || string.IsNullOrWhiteSpace(user.Email))
                    continue;

                var email = user.Email;
                var fullName = $"{user.FirstName} {user.LastName}";
                var date = appointment.Date.ToString("MMMM dd, yyyy");
                var time = appointment.Time.ToString(@"hh\:mm");

                string subject = "Appointment Reminder - MediCore Medical System";
                string body = $@"
                    <html>
                    <body style='font-family:Segoe UI, sans-serif; background-color:#f0f8ff; padding:20px; color:#1a3e5d;'>
                        <div style='max-width:600px; margin:auto; background:white; padding:30px; border-radius:10px; box-shadow:0 0 10px rgba(0,0,0,0.1);'>
                            <h2 style='color:#007BFF; margin-bottom:20px;'>MediCore Medical System</h2>
                            <p>Dear <strong>{fullName}</strong>,</p>
                            <p>This is a reminder that you have an appointment scheduled for:</p>
                            <ul style='line-height:1.6; font-size:16px;'>
                                <li><strong>Date:</strong> {date}</li>
                                <li><strong>Time:</strong> {time}</li>
                            </ul>
                            <p>Please arrive at least 10 minutes early. If you need to reschedule, contact us as soon as possible.</p>
                            <p style='margin-top:30px;'>Best regards,<br/>The MediCore Team</p>
                            <hr style='margin:30px 0; border:none; border-top:1px solid #ddd;'/>
                            <footer style='font-size:12px; color:#777;'>This is an automated reminder. Please do not reply to this email.</footer>
                        </div>
                    </body>
                    </html>";

                SendReminderEmail(email, subject, body); 
            }
        }
    }
}
