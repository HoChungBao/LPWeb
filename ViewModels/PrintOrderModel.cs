using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class PrintOrderModel
    {
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string LinkVideo { get; set; }
       public List<UploadFileApiResult> Documents { get; set; }
    }
}
