﻿using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class BusinessTripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timer { get; set; }
        public string Customer { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string UserApproved { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UserCreated { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public Guid? Token { get; set; }

        public AspNetUsers UserCreatedNavigation { get; set; }
    }
}
