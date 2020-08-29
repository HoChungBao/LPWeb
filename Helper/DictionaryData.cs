using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Helper
{
    public static class DictionaryData
    {
        public static Dictionary<string, string> ProjectType { get; set; }

        public static string GetDataType(string type)
        {
            if (ProjectType == null)
            {
                ProjectType = new Dictionary<string, string>
                {
                    {"audit", "Audit"},
                    {"khaosat", "Khảo sát"},
                    {"nghiemthu", "Nghiệm thu"},
                    {"Sanxuat", "Sản xuất"},
                    {"Posm", "POSM"},
                    {"thicong", "Thi công"},
                    {"thuevitri", "Thuê vị trí"},
                    {"baohanhbaotri", "Bảo hành/ bảo trì"},
                    {"thaodo", "Tháo dỡ"},
                    {"all", "Trọn gói"}
                };

            }
          
            if (ProjectType.ContainsKey(type))
            {
                return ProjectType[type];
            }
            return "Không có thông tin";
        }


    }
}
