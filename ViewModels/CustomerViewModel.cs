using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            History = new HashSet<History>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string Contact { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Pic { get; set; }
        public string Position { get; set; }
        public DateTime? DayCall
        {
            get
            {
                if (DateUpdated != null)
                    return dayCustomer();
                return new DateTime();
            }
        }
        public int? UnevenDay
        {
            get
            {
                if (DayCall != null)
                { var d= DayCall.Value.Subtract(DateTime.Now).Days;
                    return d;
                }
                return null;
            }
        }
        public virtual AspNetUsers UserPicNavigation { get; set; }
        public string UserName => UserPicNavigation?.UserName;
        public ICollection<History> History { get; set; }
        private DateTime dayCustomer()
        {
            
            var rs = DateUpdated.Value;
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status.Equals("H"))
                {
                    return rs.AddDays(30); //Hight potential: khách hàng tiềm năng và gọi lại sau 21-30 ngày
                }
                else if (Status.Equals("L"))
                {
                    return rs.AddDays(42);//Low potential: khách hàng low potential gọi lại sau 42 ngày, những khách hàng đặt ở chế độ này sau khoảng thời gian 2-3 tháng có thể lọc và không Follow tiếp
                }
                else if (Status.Equals("F"))
                {
                    return rs.AddDays(5); //Follow up: Follow up khách hàng đang làm đề xuất hoặc đang tình trạng chờ duyệt & gọi lại sau 5 ngày, sau khi chốt được chuyển từ F sang C
                }
                else if (Status.Equals("C"))
                {
                    return rs.AddDays(14); //Current là khách hàng đã sử dụng dịch vụ & gọi lại sau 14 ngày để hỏi thăm về kết quả và kế hoạch kế tiếp & thay đổi chế độ từ C sang H
                }
            }
          
            return new DateTime();
        }

    }
}
