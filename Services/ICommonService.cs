using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;
using LienPhatERP.Entities;
using LienPhatERP.Models;
using LienPhatERP.ViewModels;

namespace LienPhatERP.Services
{
    public interface ICommonService
    {
     
        #region Static Data

        void RunCommands(IEnumerable<string> command);
        IEnumerable<string> ParseCommand(IEnumerable<string> lines);
        #endregion
        #region Dictionary
        List<DictionaryData> GetAllDictionary();
        List<DictionaryData> GetAllStatus();
        List<DictionaryData> GetAllDepartment();
        DictionaryData GetDictionaryById(int id);
        List<DictionaryData> GetAllDictionaryByType(string value);
            #endregion
        #region RegisterDateOff
        List<RegisterDateOff> GetRegisterDateOffByUser(string id);
        RegisterDateOff InsertRegisterDateOff(RegisterDateOff entity);
        RegisterDateOff UpdateRegisterDateOff(RegisterDateOff entity);
        List<AspNetUsers> GetEmailUser(string department);
        RegisterDateOff GetRegisterById(string id);
        List<RegisterDateOff> GetAllRegisterDateOff(DateTime startDate, DateTime endDate, string department, string key);
        List<RegisterDateOff> GetAllRegisterDateOffAdmin(DateTime startDate, DateTime endDate, string department, string key);
        RegisterDateOff DeleteRegisterDateOff(string id);
        RegisterDateOff DeleteRegisterDateOffAdmin(string id);
        #endregion
       
        #region Contract
        ContactFormPlan InsertContactForm(ContactFormPlan entity);
        ContactFormPlan UpdateContactForm(ContactFormPlan entity);
        bool DetelteContactForm(long id);
        List<ContactFormPlan> GetDataContactFormPlan();
        ContactFormPlan GetContactFormPlanById(long id);
        EContactForm GetContactFormById(long id);
        EContactForm DeleteContactForm(long id);
        EContactForm InsertEContactForm(EContactForm entity);
        EContactForm UpdateContactForm(EContactForm entity);
        void UpdateStatusContactForm(long id);
        List<EContactForm> GetAllEContactForm();
        EProduct InsertEProduct(EProduct entity);
        EProduct UpdatEProduct(EProduct entity);
        EProduct GeteEProductById(long id);
        List<EProduct> GetAllEProduct();
        ProductApiModel GetProductbySlug(string slug);

        #endregion

        BookingMeetingRoom InsertAppointMentMeetingRoom(BookingMeetingRoom model);
        BookingMeetingRoom UpdateAppointMentMeetingRoom(BookingMeetingRoom model);
        BookingMeetingRoom GetAppointMentMeetingRoom(int id);
        bool CheckAppointMentMeetingRoom(DateTime dateTime);
        bool DeteteAppointMentMeetingRoom(int id);
        List<BookingMeetingRoom> GetListAppointment(string room);
        BusinessTrip InsertBusinessTrip(BusinessTrip entity);
        BusinessTrip UpdateBusinessTrip(BusinessTrip entity);
        BusinessTrip GetBusinessTripById(int id);
        BusinessTrip CheckApprovedBusinessTrip(int id);
        BusinessTrip DeleteBusinessTrip(int id);      
        List<BusinessTrip> GetAllBusinessTrip(string user);
        List<BusinessTrip> GetAllBusinessTripAdmin(DateTime startDate, DateTime endDate, string department, string key);
        List<BusinessTrip> GetBusinessTripAdmin(DateTime startDate, DateTime endDate, string department, string key);
      
    
        Files InsertFile(Files entity);
        Files UpdateFile(Files entity);
        Files DeleteFile(string id);
        List<Files> GetAllFile();
        Files GetFileById(string id);
        bool LogAction(string action, string content);
    }
}
