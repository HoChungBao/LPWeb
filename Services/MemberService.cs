using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LienPhatERP.Services
{
    public class MemberService : IMemberService
    {
        private MediGroupContext _context;
        private IMemoryCache _cache;
        public MemberService(MediGroupContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public AspNetRoles AddRole(AspNetRoles entity)
        {
            var rs = _context.AspNetRoles.Add(entity);
            _context.SaveChanges();

            return rs.Entity;
        }

        public List<AspNetRoles> GetAllRole()
        {
            return _context.AspNetRoles.ToList();
        }

        public List<AspNetUsers> GetAllUser()
        {
            return _context.AspNetUsers.Include(x=>x.AspNetUserRoles).ToList();
        }

        public AspNetUserRoles GetUserRoleById(string roleId)
        {
            return _context.AspNetUserRoles.FirstOrDefault(x => x.RoleId.Equals(roleId));
        }

        public List<AspNetUserRoles> GetUserRole()
        {
            return _context.AspNetUserRoles.ToList();
        }

        public AspNetUserRoles RemoveRole(AspNetUserRoles role)
        {
            var r= _context.AspNetUserRoles.Remove(role).Entity;
            _context.SaveChanges();
            return role;
        }

        public AspNetUserRoles GetUserRolebyEmail(string username)
        {
            return _context.AspNetUserRoles.Include(x=>x.Role).Include(x=>x.User).FirstOrDefault(x => x.User.Email.Equals(username));
        }

        public AspNetUsers ChangeLock(string username)
        {
            var rs = _context.AspNetUsers.FirstOrDefault(x => x.UserName.Equals(username));
            rs.LockoutEnabled = false;
            _context.SaveChanges();
            return rs;
        }
    }
}
