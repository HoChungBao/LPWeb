using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class UserModel
    {
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }     
        public string UserName { get; set; }
        public  Guid? RoleId { get; set; }
        public bool IsManager { get; set; }
    }
}
