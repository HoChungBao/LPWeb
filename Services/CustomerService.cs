using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace LienPhatERP.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MediGroupContext _context;
        private readonly IMemoryCache _cache;
        public CustomerService(MediGroupContext context,IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public void DeleteCustomer(long id)
        {
            var rs= GetCustomerById(id);
            DeleteHistoryCustomer(rs.Id);
            _context.Customer.Remove(rs);
            _context.SaveChanges();
        }

        public void DeleteHistoryCustomer(long id)
        {
            var rs = GetHistoryCustomerById(id);
          
            _context.SaveChanges();
        }

        public List<Customer> GetAllCustomer()
        {

            var rs = _cache.Get<List<Customer>>("listcustomer");
            if (rs != null)
            {
                return rs;
            }
            rs= _context.Customer.Include(x=>x.UserPicNavigation).OrderByDescending(x => x.DateUpdated).ToList();
            _cache.Set("listcustomer", rs, TimeSpan.FromSeconds(5));
            return rs;
        }

        public List<History> GetAllHistoryCustomer()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllPic()
        {
            var rs = _cache.Get<List<string>>("pic");
            if (rs != null)
            {
                return rs;
            }
            rs = _context.AspNetUsers.Join(_context.AspNetUserRoles,u=>u.Id,r=>r.UserId,(u,r)=>new { Name = u.Name, RoleId = r.RoleId }).Join(_context.AspNetRoles,u=>u.RoleId,r=>r.Id,(u,r)=>new { Name = u.Name, Role=r.Name }).Where(x=>x.Role.Equals("Sale")).Select(x=>x.Name).ToList();
            _cache.Set<List<string>>("pic", rs, TimeSpan.FromMinutes(1));
            return rs;
        }
        public List<AspNetUsers> GetAllUser(string rolename)
        {
            var rs = _cache.Get<List<AspNetUsers>>("GetAllUser-rolename");
            if (rs != null)
            {
                return rs;
            }
            rs = _context.AspNetUsers.Where(x=>x.LockoutEnabled == false &&x.AspNetUserRoles.Select(a=>a.Role).Count(m => m.Name.Equals(rolename))>0).ToList();
            _cache.Set("GetAllUser-rolename", rs, TimeSpan.FromMinutes(1));
            return rs;
        }
        public Customer GetCustomerById(long id)
        {
            var rs = _context.Customer.Include(x=>x.UserPicNavigation).FirstOrDefault(x => x.Id == id);
            return rs;
        }

        public List<History> GetHistoryCustomerById(long id)
        {
            return null;
        }

        public Customer InsertCustomer(Customer entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.DateUpdated = DateTime.Now;
            var item = _context.Customer.Add(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public History InsertHistoryCustomer(History entity)
        {
            entity.DateUpdate = DateTime.Now;
            var customer = GetCustomerById(entity.CustomerId);
            customer.DateUpdated = DateTime.Now;
          //  var item = _context.History.Add(entity);
           // _context.SaveChanges();
            return null;
        }

        public Customer UpdateCustomer(Customer entity)
        {
            entity.DateUpdated = DateTime.Now;
            var item = _context.Customer.Update(entity);
            _context.SaveChanges();
            return item.Entity;
        }

        public History UpdateHistoryCustomer(History entity)
        {
            throw new NotImplementedException();
        }
        public List<Customer> GetCustomerAgency()
        {
            return _context.Customer.Include(x => x.UserPicNavigation).OrderByDescending(x=>x.DateUpdated).Where(x => x.IsAgency).ToList();
        }
        #region Contract
        public Contract InsertContract(Contract entity, List<PaymentContract> paymentContracts)
        {
            entity.PaymentContract = new List<PaymentContract>();
            var contract = _context.Contract.Add(entity).Entity;
            if (paymentContracts.Count > 0)
            {
                foreach (var item in paymentContracts)
                {
                    item.ContractId = contract.Id;
                    item.DateUpdate = DateTime.Now;
                }
                _context.PaymentContract.AddRange(paymentContracts);
            }
            _context.SaveChanges();
            return contract;
        }

        public Contract UpdateContract(Contract entity, List<PaymentContract> paymentContracts)
        {
            var contract = _context.Contract.Update(entity).Entity;
            if (paymentContracts.Count > 0)
            {
                foreach (var item in paymentContracts)
                {
                    item.ContractId = contract.Id;
                    item.DateUpdate = DateTime.Now;
                }
                _context.PaymentContract.AddRange(paymentContracts);
            }
            _context.SaveChanges();
            return contract;
        }

        public List<Contract> GetAllContractById(long id)
        {
            return _context.Contract.Where(x => x.CustomerId == id).Include(x => x.Customer).Include(x => x.UserCreatedNavigation).Include(x=>x.PaymentContract).ToList();
        }

        public Contract GetContractById(long id)
        {
            return _context.Contract.Include(x=>x.PaymentContract).FirstOrDefault(x => x.Id == id);
        }
        public Contract DeleteContract(long id)
        {
            var rs = GetContractById(id);
            rs.Status = "0";
            _context.SaveChanges();
            return rs;
        }

        public ContactFormPlan ContactFormPlan(ContactFormPlan model)
        {
            model.DateCreated = DateTime.Now;
            var rs = _context.ContactFormPlan.Add(model);
            _context.SaveChanges();
            return rs.Entity;
        }
        #endregion
    }
}
