using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class EContactFormModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public long? ProductId { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? DateUpdate { get; set; }

        public EProduct Product { get; set; }

        public void Update(EContactForm rs)
        {
            rs.Name =Name;
            rs.Address = Address;
            rs.Email =Email;
            rs.Status = Status;           
            rs.Note = Note;
            rs.Phone =Phone;
            rs.ProductId = ProductId;
        }
    }
}
