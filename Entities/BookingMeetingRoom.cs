using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class BookingMeetingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Hour { get; set; }
        public double Durations { get; set; }
        public string UserCreated { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Room { get; set; }

        public AspNetUsers UserCreatedNavigation { get; set; }
    }
}
