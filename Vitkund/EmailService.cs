
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Vitkund
{
    public class EmailService
    {
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // Define the sender email credentials
                string fromEmail = "mailto:hello@scholarsacademichelp.com"; // 
                string fromPassword = "#Sami@302"; // 

                // Create a new MailMessage object
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587) // Replace with your SMTP server and port
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };

                // Send the email
                smtpClient.Send(mail);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }



        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.office365.com") // SMTP server for Office 365
                {
                    Port = 587, // Correct port for secure SMTP connection (STARTTLS)
                    Credentials = new NetworkCredential("hello@scholarsacademichelp.com", "#Sami@302"), // SMTP credentials
                    EnableSsl = true, // Ensure SSL/TLS is enabled
                };

                // Create the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("hello@scholarsacademichelp.com"), // Your email address
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true, // Set to true if you are sending an HTML email
                };

                mailMessage.To.Add(toEmail); // Recipient email address

                // Send the email asynchronously
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }

}