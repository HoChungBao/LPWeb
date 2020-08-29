using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Contants
{
    public enum OrderStatus
    {
     
        New,
        Proccessing,
        Success,
        Air,
        RequireAir,
        Danger,
        Pending,
        NoAuthorization,
        Delete
    }
    public enum OrderDetailStatus
    {
        Approved,
        Reject,
        Proccessing,
       
    }
    public class ResultStatus
    {
        public static string Success = "Thành công !";
        public static string Fail = "Có lỗi xãy ra, Vui lòng kiểm tra lại thông tin !";
        public static object ReturnTrue()
        {
            return new
            {
                Result = true,
                Message = Success
            };
        }
        public static object ReturnFalse()
        {
            return new
            {
                Result =false,
                Message = Fail
            };
        }
    }
}
