using LienPhatERP.Entities;
using LienPhatERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class InfomationModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string MainPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public WorkModel Work { get; set; }
        public InfomationModel()
        { 
        }
        public InfomationModel(Infomation model)
        {
            if(model!= null)
            {
                Id = model.Id;
                Address = model.Address;
                MainPhone = model.MainPhone;
                Phone = model.Phone;
                Email = model.Email;
                About = model.About;
                if (!string.IsNullOrEmpty(model.Work))
                {
                    Work = JsonHelper.DeserializeObject<WorkModel>(model.Work);
                }
                else
                {
                    Work = new WorkModel();
                }
            }
        }
    }
    public class WorkModel
    {
        public string InWeek { get; set; }
        public string Saturday { get; set; }
        public string WeedKed { get; set; }
    }
}
