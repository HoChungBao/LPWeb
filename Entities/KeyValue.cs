using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class KeyValue
    {
        public KeyValue()
        {
            InverseParentKeyCodeNavigation = new HashSet<KeyValue>();
        }

        public string KeyCode { get; set; }
        public int KeyTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ParentKeyCode { get; set; }
        public int? SortOrder { get; set; }

        public KeyValue ParentKeyCodeNavigation { get; set; }
        public ICollection<KeyValue> InverseParentKeyCodeNavigation { get; set; }
    }
}
