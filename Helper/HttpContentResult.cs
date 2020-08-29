using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LienPhatERP.Helper
{
    public class HttpContentResult<T>
    {
        public bool Result { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string SysMessage { get; set; }
        public int TotalItem { get; set; }
        public T Data { get; set; }

    }
    public class HttpContentResultPaged<T>
    {

        public bool Result { get; set; }
        
        public string StatusCode { get; set; }
       
        public string Message { get; set; }
      
        public string SysMessage { get; set; }
  
        public int TotalItem { get; set; }
    
        public int TotalPage { get; set; }

        public T Data { get; set; }

    }
}
