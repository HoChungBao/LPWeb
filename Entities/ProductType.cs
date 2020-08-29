using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PressCode { get; set; }
        public string Documentrequired { get; set; }
        public string Note { get; set; }
    }
}
