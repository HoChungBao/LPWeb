using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Helper;

namespace LienPhatERP.ViewModels
{
    public class OrderViewModel:OrderModel
    {
        public string TotalMoneyFormat
        {
            get
            {
                return StaticHelper.FormatMoney(TotalMoney);
            } 
        }
        public string CreateDateFormat
        {
            get
            {
                return StaticHelper.FormatDatetime(CreatedDate.ToString());
            }
        }
        public string StartDateFormat
        {
            get
            {
                if (Status.Equals(OrderStatus.Success))
                {
                    return StaticHelper.FormatDatetime(OfficalStartDate.ToString());
                }
               return StaticHelper.FormatDatetime(ExpectedStartDate.ToString());
            }
        }
        public string EndDateFormat
        {
            get
            {
                if (Status.Equals(OrderStatus.Success))
                {
                    return StaticHelper.FormatDatetime(OfficalEndDate.ToString());
                }
                return StaticHelper.FormatDatetime(ExpectedEndDate.ToString());
            }
        }
    }
}
