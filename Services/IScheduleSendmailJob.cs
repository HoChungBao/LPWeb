using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.Services
{
   public interface IScheduleSendmailJob
   {
       List<Contract> SendMailContractAsync();
       bool InsertMailSended(List<EmaiSended> list);
       List<ContactFormPlan> SendMailContactFormAsync();
        //   bool SendMail();
    }
}
