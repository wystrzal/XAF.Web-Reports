using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Web
{
    public class EmailService
    {
        private readonly string userName;
        private readonly string password;

        public EmailService(string emailUserName, string emailPassword)
        {
            userName = emailUserName;
            password = emailPassword;
        }

        public void SendEmail(MemoryStream memoryStream, string sendTo)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(userName);
            message.To.Add(new MailAddress(sendTo));
            message.Subject = "Dokument";
            message.Body = "Dokument utworzony: " + DateTime.Now.ToLongDateString();
            message.Attachments.Add(new Attachment(memoryStream, "dokument.pdf"));
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(userName, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
