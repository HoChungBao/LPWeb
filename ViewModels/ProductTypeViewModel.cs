using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;
using LienPhatERP.Helper;

namespace LienPhatERP.ViewModels
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  PressCode { get; set; }
        public List<PressCodes> PressCodes { get; set; }

        public string Documentrequired { get; set; }
        public List<PressCodes> Documentrequireds { get; set; }
        public string Note { get; set; }

        public ProductTypeViewModel()
        {

        }
        public ProductTypeViewModel(ProductType model)
        {
            Id = model.Id;
            Name = model.Name;
            PressCode = model.PressCode;
            if (!string.IsNullOrEmpty(PressCode))
            {
                PressCodes = JsonHelper.DeserializeObject<List<PressCodes>>(PressCode);
            }
            Documentrequired = model.Documentrequired;
            if (!string.IsNullOrEmpty(Documentrequired))
            {
                Documentrequireds = JsonHelper.DeserializeObject<List<PressCodes>>(Documentrequired);
            }
            Note = model.Note;
        }
    }

    public class PressCodes
    {
        public string Name { get; set; }
        public string SampleDoc { get; set; }
    }
}
