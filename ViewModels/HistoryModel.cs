using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class HistoryModel
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }
        public AspNetUsers UserUpdateNavigation { get; set; }
    }
}
