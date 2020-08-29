using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace LienPhatERP.Services
{
    public class ScheduleSendmailJob:IScheduleSendmailJob
    {
        private readonly MediGroupContext _context;
     
      
        public ScheduleSendmailJob(MediGroupContext context)
        {
            _context = context;
           
        }
        public  List<Contract> SendMailContractAsync()
        {
            var listContract =
                _context.EmaiSended.Where(x => x.CreatedDate>= DateTime.Now.AddDays(-7)&&x.Type.Equals(TypeEmail.CONTRACTSTAFF_WARNING.ToString())).Select(x=>x.ForId).ToList();
            return _context.Contract.Include(x=>x.Customer).Include(x=>x.UserCreatedNavigation).Where(x => !listContract.Contains(x.Id)&& x.EndDate <= DateTime.Now.AddDays(7)).ToList();
       
        }

        public bool InsertMailSended(List<EmaiSended> list)
        {
          _context.EmaiSended.AddRange(list);
            _context.SaveChanges();
            return true;
        }

        public List<ContactFormPlan> SendMailContactFormAsync()
        {
            var listContract =
                _context.EmaiSended.Where(x => x.CreatedDate >= DateTime.Now.AddHours(-4) && x.Type.Equals(TypeEmail.CONTACTFORM_NEW.ToString())).Select(x => x.ForId).ToList();
            return _context.ContactFormPlan.Where(x => !listContract.Contains(x.Id) && x.DateCreated >=DateTime.Now.AddHours(-1)).ToList();

        }
    }
}
