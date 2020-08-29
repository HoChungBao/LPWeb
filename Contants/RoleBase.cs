using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Contants
{
   public enum RoleBase
    {

        Admin,
        ClientService,
        HospitalCoodinator,
        HospitalPartner,
        SystemOperator,
        Content,
        Accountant,
        Sale,
        FieldMakertingService,
        Hrm,
        Marketing,
        FieldTech
    }
    public enum ActionBase
    {
       Get,
       Edit,
       Insert,
       Delete
    }
    public enum TypeVideo
    {
        NOIDUNGBV,
        QUANGCAO
    }
    public enum TypeEmail
    {
        CONTRACT_WARNING,
        CONTRACTSTAFF_WARNING,
        CONTACTFORM_NEW,
        ORDER_NEW,
    }
}
