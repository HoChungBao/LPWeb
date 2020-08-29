using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class LogAction
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public string LogContent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
