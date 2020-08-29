using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.Services
{
   public interface IMemberService
   {
       List<AspNetUsers> GetAllUser();
       List<AspNetRoles> GetAllRole();
       AspNetRoles  AddRole(AspNetRoles entity);
        List<AspNetUserRoles> GetUserRole();
        AspNetUserRoles RemoveRole(AspNetUserRoles role);
        AspNetUserRoles GetUserRoleById(string roleId);
       AspNetUserRoles GetUserRolebyEmail(string username);
        AspNetUsers ChangeLock(string username);
    }
}
