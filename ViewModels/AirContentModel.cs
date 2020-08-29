using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class AirContentModel
    {
        public AirContentModel()
        {
            AirContentDetail = new HashSet<AirContentDetail>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Files { get; set; }
        public DateTime StartDate{ get { return DateTime.ParseExact(StrStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public DateTime EndDate { get { return DateTime.ParseExact(StrEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); } set { } }
        public string Status { get; set; }
        public int Durations { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Type { get; set; }
        public string CsCreenId { get; set; }
        public string StrStartDate { get; set; }
        public string StrEndDate { get; set; }
        public string Brand { get; set; }
        public ICollection<AirContentDetail> AirContentDetail { get; set; }
    }
    public class AirContentViewModel
    {
        public AirContentViewModel()
        {
            AirContentDetail = new HashSet<AirContentDetailView>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Files { get; set; }
        public string Status { get; set; }
        public int Durations { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Brand { get; set; }

        public ICollection<AirContentDetailView> AirContentDetail { get; set; }
    }
    public class AirContentDetailView
    {
        public long Id { get; set; }
        public long CsScreenId { get; set; }
        public string Status { get; set; }
        public long AirId { get; set; }
        public string Name { get; set; }
    }
    public class MeetingRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AppointmentDateStr { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Hour { get; set; }
        public double Durations { get; set; }
        public string UserCreated { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Room { get; set; }
        public AspNetUsers UserCreatedNavigation { get; set; }
    }
    public class MeetingRoomViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string room { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}
