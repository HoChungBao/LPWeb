using LienPhatERP.Entities;
using LienPhatERP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    public interface IOutletStoreService
    {
        MOutletStore InsertDrugStore(MOutletStore entity);
        MOutletStore UpdateDrugStore(MOutletStore entity);
        MOutletStore DeleteDrugStore(MOutletStore entity);
        MOutletStore GetDrugStoreById(long id);
        List<MOutletStore> GetAllDrugStore();
        List<MOutletStore> GetAllDrugStoreNotLocation();
        void UpdateLocationOutlet(string model);
        List<OutletReportModel> GetAllOutletReport(ReportModel model);
        int ComparseMediGroup(string medigroup);
        List<MOutletStore> GetAllDrugStoreActive();
        void UpdateOutletListUser(List<MOutletStore> mOutlet, List<MOutletStoreUser> mOutletStoreUser);
        void UpdateOutletByUser(MOutletStore mOutlet, MOutletStoreUser mOutletStoreUser);
        List<MOutletStore> GetrugStoreByListId(List<long> id);

        List<MOutletStoreUser> GetAllOutletStoreUser(string user);
        List<MOutletStoreUser> GetAllOutletStoreUser();
        List<MOutletStoreUser> GetAllOutletStoreUserByUpdate();
        MOutletStoreUser GetOutletStoreUserById(long id);
        MOutletStoreUser UpdateOutletStoreUser(MOutletStoreUser entity);
        void CreateOutletByUser(string user, string userAdmin, List<string> idOutlet);
        void CreateAllOutletByUser(string user,string userAdmin);

        MProject InsertProjectDrug(MProject entity);
        MProject UpdateProjectDrug(MProject entity);
        MProject DeleteProjectDrug(MProject entity);
        MProject GetProjectDrug(int id);
        MProject GetProjectInclude(int id);
        List<MProjectCustomerModel> GetAllProject();
        MProject ProjectMediGroupCode(MProject project, List<MProjectDetail> detail);

        MProjectDetail InsertProjectDetail(MProjectDetail entity);
        void UpdateListProjectDetail(List<MProjectDetail> entity);
        void UpdateProjectDetail(MProjectDetail entity);
        MProjectDetail DeleteProjectDetail(MProjectDetail entity);
        MProjectDetail GetProjectDetail(long id);
        List<MProjectDetail> GetProjectDetailById(int id);
        List<MProjectDetail> GetProjectDetailByIdNotMediGroup(int id);
        List<MProjectDetail> GetAllProjectDetailMediGroup();
        List<MProjectDetail> MapProjectDetail(List<MProjectDetail> detail);
        List<MProjectDetail> GetProjectDetailByMediGroup(string medigroup);
        void InsertDrugProjectDetail(List<MProjectDetail> projectDetail,List<MOutletStore> drug);
        List<MProjectDetail> GetAllProjectDetail();

        MComponent InsertComponent(MComponent entity);
        MComponent UpdateComponent(MComponent entity);
        MComponent DeleteComponent(int id);
        MComponent GetComponentById(int id);
        List<MComponent> GetAllComponent();

        MDrugLabel InsertDrugLabel(MDrugLabel entity);
        MDrugLabel UpdateDrugLabel(MDrugLabel entity);
        MDrugLabel DeleteDrugLabel(MDrugLabel entity);
        MDrugLabel GetDrugLabelById(int id);
        List<MDrugLabel> GetAllDrugLabel();
        List<MOutletStore> GetAllDrugStorebyName(string keyword);
        List<MProjectDetail> GetAllProjectDetailMediGroupbyDrug(string medigroupCode);

    }
}
