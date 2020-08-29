using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Dataflow;
using LienPhatERP.Entities;
using LienPhatERP.Contants;
using LienPhatERP.Entities;
using Microsoft.Extensions.Caching.Memory;
using LienPhatERP.ViewModels;
using Microsoft.EntityFrameworkCore;
using LienPhatERP.Models;

namespace LienPhatERP.Services
{
    public class CommonService : ICommonService
    {
        private readonly MediGroupContext _context;
        private readonly IMemoryCache _cache;
        public CommonService(MediGroupContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

   

        #region  CreenDs
        public bool DeleteCreens(long id)
        {
            var cs = _context.CsCreens.FirstOrDefault(x => x.Id == id);
            if (cs == null) return false;
            _context.CsCreens.Remove(cs);
            _context.SaveChanges();
            return true;

        }
        public List<CsCreens> GetCreenses(string provinceId, string districtCode)
        {
            var rs = _context.CsCreens.Where(x =>
                    (string.IsNullOrEmpty(provinceId) || x.Province.Equals(provinceId))
                    && (string.IsNullOrEmpty(districtCode) || x.District.Equals(districtCode)))
                .ToList();
            return rs;
        }

        public CsCreens InsertCsCreens(CsCreens entity)
        {
            var item = _context.CsCreens.Add(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public bool UpdateCreens(CsCreens entity)
        {
            _context.CsCreens.Update(entity);
            _context.SaveChanges();
            return true;
        }

        public CsCreens GetCsCreenbyId(long id)
        {
            return _context.CsCreens.FirstOrDefault(x => x.Id == id);
        }
        public CsCreens GetCsCreenbyEmail(string email)
        {
            return _context.CsCreens.FirstOrDefault(x => x.Email.Equals(email));
        }

        public List<string> GetSpecialist()
        {
            List<string> cacheSpeciaList = null;
            if (_cache.TryGetValue("cacheSpeciaList", out cacheSpeciaList))
            {
                return cacheSpeciaList;
            }
            else
            {
                var item = _context.CsCreens.Where(x => x.IsHospital).Select(x => x.Speciality).ToList().Distinct().ToList();
                if (item.Count >0)
                {
                    _cache.Set("cacheSpeciaList", item, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }

                return item;
            }
           
        }
        public List<string> GetHospitalName()
        {
            List<string> cacheHospitalName = null;
          
            if (_cache.TryGetValue("cacheHospitalName", out cacheHospitalName))
            {
                return cacheHospitalName;
            }
            else
            {
                var item = _context.CsCreens.Where(x => x.IsHospital).Select(x => x.Name).ToList().Distinct().ToList();
                if (item.Count > 0)
                {
                    _cache.Set("cacheHospitalName", item, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }
                return item;
            }
        }
        public List<int> GetTrafficDaily()
        {
            List<int> cacheTrafficDaily = null;
            if (_cache.TryGetValue("cacheTrafficDaily", out cacheTrafficDaily))
            {
                return cacheTrafficDaily;
            }
            else
            {
                var item = _context.CsCreens.Where(x => x.IsHospital).Select(x => x.TrafficDaily).ToList().Distinct().ToList();
                if (item.Count > 0)
                {
                    _cache.Set("cacheTrafficDaily", item, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }
                return item;
            }
        }

   

        public ContactFormPlan InsertContactForm(ContactFormPlan entity)
        {
            entity.DateCreated = DateTime.Now;
           
            var item = _context.ContactFormPlan.Add(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public ContactFormPlan UpdateContactForm(ContactFormPlan entity)
        {
            var item = _context.ContactFormPlan.Update(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public bool DetelteContactForm(long id)
        {
            var rs = _context.ContactFormPlan.FirstOrDefault(x => x.Id == id);
            if (rs == null) return false;
            _context.ContactFormPlan.Remove(rs);
            _context.SaveChanges();
            return true;

        }

        public List<ContactFormPlan> GetDataContactFormPlan()
        {
            return _context.ContactFormPlan.OrderByDescending(x=>x.DateCreated).ToList();
        }

        public ContactFormPlan GetContactFormPlanById(long id)
        {
            return _context.ContactFormPlan.FirstOrDefault(x => x.Id == id);
        }

   
        public bool LogAction(string action, string content)
        {
           var log = new LogAction()
           {
               Action = action,
               LogContent = content,
               CreateDate = DateTime.Now
           };
            _context.LogAction.Add(log);
            _context.SaveChanges();
            return true;
        }

        //public List<CsCreenPartialViewodel> GetCsCreens()
        //{
        //    var css = _context.CsCreens.AsQueryable();
        //    var provinces = _context.KeyValue.AsQueryable();
        //    var rs = from cs in css
        //        join province in provinces on cs.Province equals province.Value
        //        select new CsCreenPartialViewodel
        //        {
        //            Id = cs.Id,
        //            Name = cs.Name,
        //            IsHospital = cs.IsHospital,
        //            Speciality =  cs.Speciality,
        //            Address = cs.Address,
        //            District = cs.District,
        //            Lat = cs.Lat,
        //            Ln = cs.Ln,
        //            TotalLed = cs.TotalLed,
        //            Province = province.Name,
        //            TrafficDaily = cs.TrafficDaily

        //        };
        //    return rs.ToList();

        //}
       

       

     

        #endregion
        public void RunCommands(IEnumerable<string> commands)
        {
            foreach (var command in commands)
            {
                _context.Database.ExecuteSqlCommand(command);
            }
        }
        public IEnumerable<string> ParseCommand(IEnumerable<string> lines)
        {
            var sb = new StringBuilder();
            var commands = new List<string>();
            foreach (var line in lines)
            {
                if (string.Equals(line, "GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (sb.Length > 0)
                    {
                        commands.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        sb.Append(line);
                    }
                }
            }

            return commands;
        }
        #region Distionary
        public List<DictionaryData> GetAllDictionary()
        {
            var rs = _cache.Get<List<DictionaryData>>("dictionary");
            if (rs != null)
            {
                return rs;
            }
            rs = _context.DictionaryData.Where(x=>x.Type.Equals("C")).ToList();
            _cache.Set<List<DictionaryData>>("dictionary", rs, TimeSpan.FromMinutes(1));
            return rs;
        }

        public List<DictionaryData> GetAllStatus()
        {
            var rs = _cache.Get<List<DictionaryData>>("status");
            if (rs != null)
            {
                return rs;
            }
            rs = _context.DictionaryData.Where(x => x.Type.Equals("CustomerStatus")).ToList();
            _cache.Set<List<DictionaryData>>("status", rs, TimeSpan.FromMinutes(1));
            return rs;
        }

        public List<DictionaryData> GetAllDepartment()
        {
            var rs = _cache.Get<List<DictionaryData>>("department");
            if (rs != null)
            {
                return rs;
            }
            rs = _context.DictionaryData.Where(x => x.Type.Equals("D")).ToList();
            _cache.Set<List<DictionaryData>>("department", rs, TimeSpan.FromMinutes(1));
            return rs;
        }

     
        public DictionaryData GetDictionaryById(int id)
        {
            return GetAllDictionary().FirstOrDefault(x => x.Id == id);
        }
        public List<DictionaryData> GetAllDictionaryByType(string value)
        {
            var rs = _cache.Get<List<DictionaryData>>(value);
            if (rs != null)
            {
                return rs;
            }
            rs = _context.DictionaryData.Where(x => x.Type.Equals(value)).ToList();
            _cache.Set<List<DictionaryData>>(value, rs, TimeSpan.FromMinutes(1));
            return rs;
        }
        #endregion


        #region RegisterDateOff
        public List<RegisterDateOff> GetRegisterDateOffByUser(string id)
        {
            return _context.RegisterDateOff.Where(x => x.UserCreate.Equals(id)).Include(x=>x.UserApprovedNavigation).OrderByDescending(x=>x.DateCreated).ToList();
        }

        public RegisterDateOff InsertRegisterDateOff(RegisterDateOff entity)
        {
            entity.Token = Guid.NewGuid().ToString();
            entity.DateCreated = DateTime.Now;
            entity.DateApproved = DateTime.Now;
            entity.Status = "0";
            var rs = _context.RegisterDateOff.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public RegisterDateOff UpdateRegisterDateOff(RegisterDateOff entity)
        {
            entity.DateApproved = DateTime.Now;
            var rs = _context.RegisterDateOff.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public List<AspNetUsers> GetEmailUser(string department)
        {
            return _context.AspNetUsers.Where(x => x.IsManager && (!string.IsNullOrEmpty(x.CompanyName)  && x.CompanyName.Equals(department))).ToList();
        }

        public RegisterDateOff GetRegisterById(string id)
        {
            return _context.RegisterDateOff.FirstOrDefault(x => x.Token.Equals(id));
        }

        public List<RegisterDateOff> GetAllRegisterDateOff(DateTime startDate, DateTime endDate, string department, string key)
        {
            return _context.RegisterDateOff.Where(x => (x.DateCreated >= startDate && x.DateCreated <= endDate) && ((!string.IsNullOrEmpty(x.UserCreateNavigation.CompanyName) && x.UserCreateNavigation.CompanyName.Equals(department) || string.IsNullOrEmpty(department)) && (x.Name.Contains(key) || string.IsNullOrEmpty(key)))).Include(x => x.UserCreateNavigation).ToList();
        }
        public List<RegisterDateOff> GetAllRegisterDateOffAdmin(DateTime startDate, DateTime endDate, string department, string key)
        {
            return _context.RegisterDateOff.Where(x => (x.DateCreated >= startDate && x.DateCreated <= endDate) && ((!string.IsNullOrEmpty(x.UserCreateNavigation.CompanyName) && x.UserCreateNavigation.CompanyName.Equals(department)) && (x.Name.Contains(key) || string.IsNullOrEmpty(key)))).Include(x => x.UserCreateNavigation).ToList();
        }
        public RegisterDateOff DeleteRegisterDateOff(string id)
        {
            var rs = GetRegisterById(id);
            _context.RegisterDateOff.Remove(rs);
            _context.SaveChanges();
            return rs;
        }
        public RegisterDateOff DeleteRegisterDateOffAdmin(string id)
        {
            var rs = GetRegisterById(id);
            rs.Status = "2";
            _context.SaveChanges();
            return rs;
        }
        #endregion

        public EContactForm InsertEContactForm(EContactForm entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.DateUpdate = DateTime.Now;
            var rs = _context.EContactForm.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public EContactForm UpdateContactForm(EContactForm entity)
        {
            entity.DateUpdate= DateTime.Now;
            _context.EContactForm.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public EContactForm GetContactFormById(long id)
        {
            var rs = _context.EContactForm.FirstOrDefault(x => x.Id == id);
            return rs;
        }

        public List<EContactForm> GetAllEContactForm()
        {
            return _context.EContactForm.Include(x => x.Product).ToList();
        }
        public void UpdateStatusContactForm(long id)
        {
            var rs = GetContactFormById(id);
            rs.Status = "1";
            _context.SaveChanges();
        }

        public EContactForm DeleteContactForm(long id)
        {
            var rs = GetContactFormById(id);
            _context.EContactForm.Remove(rs);
            return rs;
        }

        public EProduct InsertEProduct(EProduct entity)
        {
            entity.ReviewsCount = 0;
            entity.IsDeleted = false;
            entity.HasOptions = true;
            entity.IsVisibleIndividually = true;
            entity.IsFeatured = true;
            entity.IsAllowToOrder = true;
            entity.IsCallForPricing = true;
            entity.StockQuantity = 0;
            entity.DisplayOrder = 0;
            entity.CreatedOn = DateTimeOffset.Now;
            entity.UpdatedOn = DateTimeOffset.Now;
            var rs = _context.EProduct.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public EProduct UpdatEProduct(EProduct entity)
        {
            entity.UpdatedOn = DateTimeOffset.Now;
            _context.EProduct.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public EProduct GeteEProductById(long id)
        {
            var rs = _context.EProduct.FirstOrDefault(x => x.Id == id);
            return rs ;
        }

        public List<EProduct> GetAllEProduct()
        {
            return _context.EProduct.Include(x=>x.EContactForm).ToList();
        }
        public ProductApiModel GetProductbySlug(string slug)
        {
            var p = _context.EProduct.FirstOrDefault(x => x.Slug == slug);

            if (p == null) return null;
            {
                var list = _context.EProduct.Where(x => x.Id != p.Id && x.IsDeleted == false).OrderByDescending(x=>x.CreatedOn).Take(4).ToList();
                var rs = new ProductApiModel()
                {
                  //  prodcutModel = p,
                    arrayProduct = list.Select(m=> new RelatedProduct() { Id = m.Id, Image = m.ThumbnailImage, Title = m.MetaTitle }).ToList()
                };
                return rs;
            }
        }

        public BookingMeetingRoom InsertAppointMentMeetingRoom(BookingMeetingRoom model)
        {
           var rs=  _context.BookingMeetingRoom.Add(model);
            _context.SaveChanges();
            return rs.Entity ;
        }

        public BookingMeetingRoom UpdateAppointMentMeetingRoom(BookingMeetingRoom model)
        {
            var rs= _context.BookingMeetingRoom.Update(model);
            _context.SaveChanges();
            return rs.Entity;
        }

        public BookingMeetingRoom GetAppointMentMeetingRoom(int id)
        {
            return _context.BookingMeetingRoom.FirstOrDefault(x => x.Id == id);
        }

        public bool CheckAppointMentMeetingRoom(DateTime dateTime)
        {
            var rs = _context.BookingMeetingRoom.Where(x =>
                x.AppointmentDate <= dateTime && dateTime <= x.AppointmentDate.AddHours(x.Durations)).ToList();
            return !(rs?.Count() > 0);
        }

        public bool DeteteAppointMentMeetingRoom(int id)
        {
            var rs = _context.BookingMeetingRoom.FirstOrDefault(x => x.Id == id);
            if (rs != null) _context.BookingMeetingRoom.Remove(rs);
            _context.SaveChanges();
            return true;
        }

        public List<BookingMeetingRoom> GetListAppointment(string room)
        {
            return _context.BookingMeetingRoom.Include(x=>x.UserCreatedNavigation).Where(x => (string.IsNullOrEmpty(room) ||x.Room.Equals(room)) 
            && x.AppointmentDate >= DateTime.Today)
                .ToList();
        }
        public BusinessTrip InsertBusinessTrip(BusinessTrip entity)
        {
            entity.Status = "0";
            entity.CreatedDate= DateTime.Now;
            entity.Token = Guid.NewGuid();
           var rs = _context.BusinessTrip.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public BusinessTrip UpdateBusinessTrip(BusinessTrip entity)
        {
            var rs = _context.BusinessTrip.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public BusinessTrip GetBusinessTripById(int id)
        {
            var rs = _context.BusinessTrip.FirstOrDefault(x => x.Id == id);
            return rs;
        }

        public BusinessTrip CheckApprovedBusinessTrip(int id)
        {
            var rs = GetBusinessTripById(id);
            if (rs.Status != "1")
            {
                rs.Status = "1";
            }
            else
            {
                rs.Status = "0";
            }
            _context.SaveChanges();
            return rs;
        }

        public BusinessTrip DeleteBusinessTrip(int id)
        {
            var rs = GetBusinessTripById(id);
            rs.Status = "2";
            _context.SaveChanges();
            return rs;
        }

        public List<BusinessTrip> GetAllBusinessTrip(string user)
        {
            return _context.BusinessTrip.Where(x => x.UserCreated.Equals(user)).Include(x => x.UserCreatedNavigation).ToList();
        }

        public List<BusinessTrip> GetAllBusinessTripAdmin(DateTime startDate, DateTime endDate, string department, string key)
        {
            return _context.BusinessTrip.Where(x=>(x.CreatedDate>=startDate&&x.CreatedDate<=endDate)&&((!string.IsNullOrEmpty(x.UserCreatedNavigation.CompanyName)&&x.UserCreatedNavigation.CompanyName.Equals(department)||string.IsNullOrEmpty(department))&&(x.Name.Contains(key)||string.IsNullOrEmpty(key)))).Include(x=>x.UserCreatedNavigation).ToList();
        }
        public List<BusinessTrip> GetBusinessTripAdmin(DateTime startDate, DateTime endDate, string department, string key)
        {
            return _context.BusinessTrip.Where(x => (x.CreatedDate >= startDate && x.CreatedDate <= endDate) && ((!string.IsNullOrEmpty(x.UserCreatedNavigation.CompanyName) && x.UserCreatedNavigation.CompanyName.Equals(department)) && (x.Name.Contains(key) || string.IsNullOrEmpty(key)))).Include(x => x.UserCreatedNavigation).ToList();
        }

     
       

       

        public Files InsertFile(Files entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DateCreated = DateTime.Now;
            var rs = _context.Files.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public Files UpdateFile(Files entity)
        {
            _context.SaveChanges();
            return entity;
        }

        public Files DeleteFile(string id)
        {
            var rs = GetFileById(id);
            _context.Files.Remove(rs);
            _context.SaveChanges();
            return rs;
        }

        public List<Files> GetAllFile()
        {
            return _context.Files.OrderByDescending(x => x.DateCreated).ToList();
        }

        public Files GetFileById(string id)
        {
            var rs = _context.Files.FirstOrDefault(x => x.Id.Equals(Guid.Parse(id)));
            return rs;
        }

        public List<CsCreenPartialViewodel> GetCsCreens()
        {
            throw new NotImplementedException();
        }
    }
}
