using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class ComboDetails
    {
        public long Id { get; set; }
        public long CsScreenId { get; set; }
        public string CsScreenName { get; set; }
        public int AmountFull { get; set; }
        public decimal PriceFull { get; set; }
    }
}
