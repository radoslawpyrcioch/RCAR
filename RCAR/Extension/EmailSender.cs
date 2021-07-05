using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RCAR.Extension
{
    public class EmailSender
    {
        public static async Task<bool> MailSender(string userEmail, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.To.Add(userEmail);
                message.From = new MailAddress("testprogramcar1992@gmail.com", "Aplikacja RCarManager");
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "testprogramcar1992@gmail.com",
                        Password = "Start123!"
                    };

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

