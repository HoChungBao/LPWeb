using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Services;

namespace LienPhatERP.Models
{
    public class DataModel
    {
        public string Msg { get; set; } = null;
        public List<string> LangList { get; set; }
        public string SelectedLang { get; set; } = null;

        public DataModel()
        {
            GetData Getdata = new GetData();
            LangList = Getdata.GetLanguagesList();

        }
    }
}
