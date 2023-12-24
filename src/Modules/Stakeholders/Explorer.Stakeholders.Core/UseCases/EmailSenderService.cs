using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class EmailSenderService
    {
        public void SendVerificationEmail(string to, string verificationToken)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("pswexplorer@gmail.com", "eqvw sehe rgzf nzzp");

                    string subject = "Verify Your Email";
                    string verificationLink = $"http://localhost:4200/api/user/confirm-account?token={verificationToken}";
                    string body = $"Click the following link to verify your email: {verificationLink}";

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("pswexplorer@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending verification email: {ex.Message}");
            }
        }
    }
}
