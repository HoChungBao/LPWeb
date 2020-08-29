using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace LienPhatERP.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, BodyBuilder body);
        Task SendEmailAsync(List<string> email, string subject, BodyBuilder body);
        void MessageBody(string templateEmail, out string messageBody);
    }
}
