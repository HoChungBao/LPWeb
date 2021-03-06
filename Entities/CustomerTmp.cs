﻿using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class CustomerTmp
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Contact { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Pic { get; set; }
        public string Position { get; set; }
        public string UserPic { get; set; }
        public bool IsAgency { get; set; }
    }
}
