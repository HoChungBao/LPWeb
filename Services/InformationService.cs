using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public class InformationService : IInformationService
    {
        private readonly MediGroupContext _context;
        public InformationService(MediGroupContext context)
        {
            _context = context;
        }
        public Infomation Delete(Infomation entity)
        {
            _context.Infomation.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public Infomation GetInfomationById(long id)
        {
            return _context.Infomation.FirstOrDefault(x=>x.Id==id);
        }

        public Infomation Insert(Infomation entity)
        {
            _context.Infomation.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Infomation Update(Infomation entity)
        {
            _context.Infomation.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<Infomation> GetAllInfomation()
        {
            return _context.Infomation.ToList();
        }
        public void InsertList(List<Infomation> entity)
        {
            _context.Infomation.AddRange(entity);
            _context.SaveChanges();
        }
    }
}
