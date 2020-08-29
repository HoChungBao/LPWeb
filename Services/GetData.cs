using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public class GetData
    {
        public GetData()
        {
        }

        public List<string> GetLanguagesList()
        {
            return new List<string>() {
                "c", "c++","c#","COBOL", "java", "php", "coldfusion", "javascript", "asp", "ruby"
            };
        }



    }
}
