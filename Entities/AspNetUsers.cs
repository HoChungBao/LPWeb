using System;
using System.Collections.Generic;

namespace LienPhatERP.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            BookingMeetingRoom = new HashSet<BookingMeetingRoom>();
            BusinessTrip = new HashSet<BusinessTrip>();
            Contract = new HashSet<Contract>();
            Customer = new HashSet<Customer>();
            FilesNavigation = new HashSet<Files>();
            NewsPost = new HashSet<NewsPost>();
            RegisterDateOffUserApprovedNavigation = new HashSet<RegisterDateOff>();
            RegisterDateOffUserCreateNavigation = new HashSet<RegisterDateOff>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsManager { get; set; }
        public DateTime? DateWork { get; set; }
        public string Image { get; set; }
        public string Files { get; set; }
        public DateTime? Date { get; set; }
        public string Home { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string CompanyName { get; set; }
        public string IdentityCard { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string Level { get; set; }
        public string GraduationYear { get; set; }
        public string Male { get; set; }
        public string School { get; set; }
        public string Folk { get; set; }
        public string NoContract { get; set; }
        public string NoBhxh { get; set; }
        public string CurrentContract { get; set; }
        public string Position { get; set; }
        public string UserCode { get; set; }
        public string ContractType { get; set; }
        public string Tghd { get; set; }
        public string Tgbh { get; set; }
        public string Bank { get; set; }
        public string Matrimony { get; set; }
        public string Salary { get; set; }

        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<BookingMeetingRoom> BookingMeetingRoom { get; set; }
        public ICollection<BusinessTrip> BusinessTrip { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Files> FilesNavigation { get; set; }
        public ICollection<NewsPost> NewsPost { get; set; }
        public ICollection<RegisterDateOff> RegisterDateOffUserApprovedNavigation { get; set; }
        public ICollection<RegisterDateOff> RegisterDateOffUserCreateNavigation { get; set; }
    }
}
