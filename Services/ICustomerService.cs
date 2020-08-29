using LienPhatERP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public interface ICustomerService
    {
        Customer InsertCustomer(Customer entity);
        Customer UpdateCustomer(Customer entity);
        void DeleteCustomer(long id);
        List<Customer> GetAllCustomer();
        Customer GetCustomerById(long id);
        History InsertHistoryCustomer(History entity);
        History UpdateHistoryCustomer(History entity);
        void DeleteHistoryCustomer(long id);
        List<History> GetAllHistoryCustomer();
        List<History> GetHistoryCustomerById(long id);
        List<string> GetAllPic();
        List<AspNetUsers> GetAllUser(string rolename);
        List<Contract> GetAllContractById(long id);
        Contract InsertContract(Contract entity,List<PaymentContract> paymentContracts);
        Contract UpdateContract(Contract entity,List<PaymentContract> paymentContracts);
        Contract DeleteContract(long id);
        Contract GetContractById(long id);
        List<Customer> GetCustomerAgency();
        ContactFormPlan ContactFormPlan(ContactFormPlan model);

    }
}
