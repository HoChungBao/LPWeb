using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public interface IInformationService
    {
        Infomation Insert(Infomation entity);
        Infomation Update(Infomation entity);
        Infomation Delete(Infomation entity);
        Infomation GetInfomationById(long id);
        List<Infomation> GetAllInfomation();

        void InsertList(List<Infomation> entity);
    }
}
