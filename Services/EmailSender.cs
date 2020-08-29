using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Text;

namespace LienPhatERP.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private IHostingEnvironment _env;
        public EmailSender(IHostingEnvironment env)
        {
            _env = env;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress
            ("Liên phát",
                "bao.ho@medihub.com.vn"
            ));
            mimeMessage.To.Add(new MailboxAddress
            (subject,
                email
            ));
            mimeMessage.Subject = subject; //Subject  
            mimeMessage.Body = new TextPart("html")
            {
                Text = message
               
            };
            // mimeMessage = message;
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("mail.medihub.com.vn", 465, SecureSocketOptions.Auto);
                client.Authenticate(
                    "bao.ho@medihub.com.vn",
                    "hochungbao"
                );
                await client.SendAsync(mimeMessage);
              
                await client.DisconnectAsync(true);
            }

            await Task.FromResult(0);
        }
        public async Task SendEmailAsync(string email, string subject, BodyBuilder body)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress
            ("MediHub",
                "cs@medihub.com.vn"
            ));
            mimeMessage.To.Add(new MailboxAddress
            ("KH",
                email
            ));
            mimeMessage.Subject = subject; //Subject  
            mimeMessage.Body = new TextPart("html")
            {
                Text = body.HtmlBody
             
               

            };
            //mimeMessage.Body = new TextPart("plain")
            //{
            //    Text = body.HtmlBody

            //};

            // = body.HtmlBody;
            // mimeMessage = message;
            using (var client = new SmtpClient())
            {
                client.Connect("mail.medihub.com.vn", 465, true);
                client.Authenticate(
                    "cs@medihub.com.vn",
                    "302levansy"
                );
                await client.SendAsync(mimeMessage);
                Console.WriteLine("The mail has been sent successfully !!");
                Console.ReadLine();
                await client.DisconnectAsync(true);
            }

            await Task.FromResult(0);
        }

        public async Task SendEmailAsync(List<string> emails, string subject, BodyBuilder body)
        {
            //foreach (var email in  emails)
            //{
            //    var mimeMessage = new MimeMessage();
            //    mimeMessage.From.Add(new MailboxAddress
            //    ("MediHub",
            //        "cs@medihub.com.vn"
            //    ));
            //    mimeMessage.To.Add(new MailboxAddress
            //    ("KH",
            //        email
            //    ));
            //    mimeMessage.Subject = subject; //Subject  
            //    mimeMessage.Body = new TextPart("html")
            //    {
            //        Text = message

            //    };
            //    // mimeMessage = message;
            //    using (var client = new SmtpClient())
            //    {
            //        client.Connect("mail.medihub.com.vn", 465, true);
            //        client.Authenticate(
            //            "cs@medihub.com.vn",
            //            "302levansy"
            //        );
            //        await client.SendAsync(mimeMessage);

                    
            //    }
            //}
            
            //await client.DisconnectAsync(true);
            await Task.FromResult(0);
        
       }
        public void MessageBody(string templateEmail, out string messageBody)
        {
            var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "Templates"
                             + Path.DirectorySeparatorChar.ToString()
                             + "EmailTemplate"
                             + Path.DirectorySeparatorChar.ToString()
                             + templateEmail;
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            messageBody = builder.HtmlBody;
        }
    }
}
