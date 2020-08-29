using LienPhatERP.ViewModels;
using LienPhatERP.Helper;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Contants
{
    public class Province
    {
        public static List<ProvinceViewModel> GetProvinces(IHostingEnvironment _env)
        {
           var pathToFile = _env.WebRootPath
                             + Path.DirectorySeparatorChar.ToString()
                             + "assets/demo/default/base/province.json";
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {

                    var datas = SourceReader.ReadToEnd();

                    var jsondata = JsonHelper.DeserializeObject<List<ProvinceViewModel>>(datas);

                    return jsondata;
                }

            }
            catch (Exception e)
            {
                return new List<ProvinceViewModel>();
            }
        }
    }
}
