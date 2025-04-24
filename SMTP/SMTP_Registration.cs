using System.Net;
using System.Net.Mail;
namespace MediCore.SMTP;
public class SMTP_Registration
{
    public static string GenerateVerificationCode()
    {
        Random random = new Random();
        return random.Next(1000, 9999).ToString();
    }
    public static void EmailSender(string ToAddress, string verificationCode)
    {
        string senderEmail = "nikalobjanidze014@gmail.com";
        string appPassword = "edkv wxdh dzrl sdqo"; /// APP PASSWORD
        string htmlContent = $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Your Verification Code</title>
</head>
<body style='margin: 0; padding: 0; font-family: 'Segoe UI', Roboto, Helvetica, sans-serif; background-color: #000000; color: #ffffff;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='max-width: 600px; margin: 0 auto; background-color: #000000;'>
        <!-- Header -->
        <tr>
            <td style='padding: 40px 20px 20px; text-align: center;'>
                <h1 style='color: #ffffff; font-size: 28px; margin: 0; letter-spacing: 1px;'>SECURE ACCESS</h1>
                <div style='height: 3px; width: 80px; background: linear-gradient(90deg, #0076be 0%, #00a8ff 100%); margin: 15px auto 0;'></div>
            </td>
        </tr>
        
        <!-- Content -->
        <tr>
            <td style='padding: 30px 40px;'>
                <p style='font-size: 18px; line-height: 1.6; margin: 0 0 25px;'>Welcome to our platform! Here's your one-time verification code:</p>
                
                <!-- Verification Code Box -->
                <div style='background-color: #121212; border-radius: 8px; padding: 25px; margin: 30px 0; text-align: center; border: 1px solid #333;'>
                    <div style='font-size: 42px; font-weight: 700; letter-spacing: 3px; color: #00a8ff;'>{verificationCode}</div>
                    <div style='font-size: 14px; color: #aaa; margin-top: 10px;'>Expires in 15 minutes</div>
                </div>
                
                <p style='font-size: 16px; line-height: 1.6; margin: 0 0 15px; color: #ccc;'>Simply enter this code where prompted to complete your verification.</p>
                
                <!-- Security Note -->
                <div style='background-color: #121212; border-left: 4px solid #0076be; padding: 15px; margin: 30px 0 0;'>
                    <p style='font-size: 14px; line-height: 1.5; margin: 0; color: #ccc;'>
                        <strong style='color: #fff;'>Security tip:</strong> Never share this code with anyone. Our team will never ask for it.
                    </p>
                </div>
            </td>
        </tr>
        
        <!-- Footer -->
        <tr>
            <td style='padding: 30px 20px; text-align: center; border-top: 1px solid #333;'>
                <p style='font-size: 14px; color: #999; margin: 0 0 10px;'>If you didn't request this, please ignore this email.</p>
                <p style='font-size: 14px; color: #999; margin: 0;'>© 2023 Your Brand. All rights reserved.</p>
            </td>
        </tr>
    </table>
</body>
</html>";

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(senderEmail);
        mail.To.Add(ToAddress);
        mail.Subject = "Verification Code";
        mail.Body = htmlContent;
        mail.IsBodyHtml = true;
        var smtpClient = new SmtpClient("smtp.gmail.com") 
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(senderEmail, appPassword),
        };
        smtpClient.Send(mail);
    }
   


}
