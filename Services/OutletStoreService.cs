using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;
using LienPhatERP.Helper;
using LienPhatERP.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LienPhatERP.Services
{
    public class OutletStoreService : IOutletStoreService
    {
        private readonly MediGroupContext _context;
        private readonly IMemoryCache _cache;

        public OutletStoreService(MediGroupContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }
        #region DrugStore
        public MOutletStore DeleteDrugStore(MOutletStore entity)
        {
            var rs = _context.MOutletStore.Remove(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MOutletStore GetDrugStoreById(long id)
        {
            return _context.MOutletStore.FirstOrDefault(x => x.Id == id);
        }
        public MOutletStore InsertDrugStore(MOutletStore entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MOutletStore.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MOutletStore UpdateDrugStore(MOutletStore entity)
        {
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MOutletStore.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public List<MOutletStore> GetAllDrugStore()
        {
            return _context.MOutletStore.ToList();
        }
        public List<MOutletStore> GetAllDrugStorebyName(string keyword)
        {
            return _context.MOutletStore.Where(x=>x.DrugStoreName.Contains(keyword)).Take(30).ToList();
        }
        public List<MOutletStore> GetAllDrugStoreNotLocation()
        {
            return _context.MOutletStore.Where(x=> string.IsNullOrEmpty(x.Latitue)|| string.IsNullOrEmpty(x.Longtitue)).Take(50).ToList();
        }
        public void UpdateLocationOutlet(string model)
        {
            var outlet = JsonHelper.DeserializeObject<List<MOutletStoreApiModel>>(model);
            var id = outlet.Select(i => i.Id).ToList();
            var rs = _context.MOutletStore.Where(x => id.Contains(x.Id)).ToList();
            rs.ForEach(x =>
            {
                var location = outlet.First(i => i.Id == x.Id);
                x.Latitue = location.Latitue;
                x.Longtitue = location.Longtitue;
            });
            _context.MOutletStore.UpdateRange(rs);
            _context.SaveChanges();
        }
        public List<OutletReportModel> GetAllOutletReport(ReportModel model)
        {
            var outletReport = _cache.Get<List<OutletReportModel>>($"reportoutlet-{model.District}-{model.Province}-{model.Label}-{model.Component}-{model.DateBeginRent.ToString()}-{model.CustomerId}-{model.CostPayForDrugStore}-{model.Type}-{model.Position}");
            if (outletReport != null)
            {
                return outletReport;
            }
            var projectDetail = (from prd in _context.MProjectDetail.Where(x => !string.IsNullOrEmpty(x.MediGroupCode))
                                 
                               
                                join project in _context.MProject on prd.ProjectId equals project.Id
                               
                                 join customer in _context.Customer on project.CustomerId equals customer.Id
                                 where
                                ( model.CustomerId == 0 || project.CustomerId == model.CustomerId)
                             
                                  && (string.IsNullOrEmpty(model.District) || prd.District.Contains(StringUtils.RemoveVietNam(model.District)))
                                 && (string.IsNullOrEmpty(model.Province) || prd.Province.Contains(StringUtils.RemoveVietNam(model.Province)))
                                 && (string.IsNullOrEmpty(model.Label) || prd.Label.Equals(model.Label))
                                 && (model.CostPayForDrugStore == 0 || prd.CostPayForDrugStore >= model.CostPayForDrugStore)
                                 && (string.IsNullOrEmpty(model.Component) || prd.Component.Equals(model.Component))
                                 && (string.IsNullOrEmpty(model.Position) || prd.Position.Equals(model.Position))
                                 && (model.DateBeginRent ==DateTime.MinValue|| prd.DateBeginRent.AddMonths(prd.MonthRent) >= model.DateBeginRent)
                                 select new OutletReportModel()
                                 {
                                     Id = prd.Id,
                                     Component = prd.Component,
                                     Label = prd.Label,
                                     Position = prd.Position,
                                     NumOfArea = prd.Hsize * prd.Vsize,
                                     Unit = prd.Unit,
                                     CostPayForDrugStore = prd.CostPayForDrugStore,
                                     CostPayForProduction = prd.CostPayForProduction,
                                     DateBeginRent = prd.DateBeginRent,
                                     MonthRent = prd.MonthRent,
                                     ProjectId = prd.ProjectId,
                                     Note = prd.Note,
                                     Images = prd.Images,
                                     DrugName = prd.DrugName,
                                     District = prd.District,
                                     Province = prd.Province,
                                     Area = prd.Area,
                                     Address = prd.Address,
                                     MediGroupCode = prd.MediGroupCode,
                                     Ward = prd.Ward,
                                     CustomerId = customer.Id,
                                     CustomerName = customer.Name,
                                     ProjectName = project.Name,                                  
                                 }).ToList();
            if (projectDetail.Count() > 0)
            {
                _cache.Set($"reportoutlet-{model.District}-{model.Province}-{model.Label}-{model.Component}-{model.DateBeginRent.ToString()}-{model.CustomerId}-{model.CostPayForDrugStore}-{model.Type}-{model.Position}", outletReport, TimeSpan.FromMinutes(1));
            }
           
            return projectDetail;
        }
        public int ComparseMediGroup(string medigroup)
        {
            return _context.MOutletStore.Where(x => x.MediGroupCode.Equals(medigroup)).Count();
        }
        public List<MOutletStore> GetAllDrugStoreActive()
        {
            return _context.MOutletStore.Where(x => x.Status.Equals("1")).ToList();
        }
        public void UpdateOutletListUser(List<MOutletStore> mOutlet, List<MOutletStoreUser> mOutletStoreUser)
        {
           _context.MOutletStore.UpdateRange(mOutlet);
           _context.MOutletStoreUser.UpdateRange(mOutletStoreUser);
           _context.SaveChanges();
        }
        public void UpdateOutletByUser(MOutletStore mOutlet, MOutletStoreUser mOutletStoreUser)
        {
            mOutlet.UpdatedDate = DateTime.Now;
            mOutletStoreUser.UpdatedDate = DateTime.Now;
            _context.MOutletStore.Update(mOutlet);
            _context.MOutletStoreUser.Update(mOutletStoreUser);
            _context.SaveChanges();
        }
        public List<MOutletStore> GetrugStoreByListId(List<long> id)
        {
            return _context.MOutletStore.Where(x => id.Contains(x.Id)).ToList();
        }
    #endregion

    #region User
    public void CreateOutletByUser(string user, string userAdmin, List<string> idOutlet)
        {
            //var idCreated = _context.MOutletStoreUser.Where(x => x.UpdatedBy.Equals(user) && idOutlet.Contains(x.Id.ToString())).Select(x => x.Id.ToString()).ToList();
            //idOutlet= idOutlet.Where(x => !idCreated.Contains(x)).ToList();
            var rs = MapOutletStoreUser(user, userAdmin, _context.MOutletStore.Where(x => idOutlet.Contains(x.Id.ToString())));
            _context.MOutletStoreUser.AddRange(rs);
            _context.SaveChanges();
        }
        public void CreateAllOutletByUser(string user, string userAdmin)
        {
            var idCreated = _context.MOutletStoreUser.Where(x => x.UpdatedBy.Equals(user)).Select(x => x.Id).ToList();
            var rs = MapOutletStoreUser(user, userAdmin, _context.MOutletStore.Where(x => !idCreated.Contains(x.Id)));
            _context.MOutletStoreUser.AddRange(rs);
            _context.SaveChanges();
        }
        private List<MOutletStoreUser> MapOutletStoreUser(string user, string userAdmin,IQueryable<MOutletStore> mOutletStore)
        {
            return mOutletStore.Select(x => new MOutletStoreUser()
             {
                 Id = x.Id,
                 Area = x.Area,
                 AvatarUrl = x.AvatarUrl,
                 BankAccount = x.BankAccount,
                 DrugStoreAddress = x.DrugStoreAddress,
                 MediGroupCode = x.MediGroupCode,
                 CreatedBy = userAdmin,
                 CreatedDate = DateTime.Now,
                 District = x.District,
                 DrugStoreName = x.DrugStoreName,
                 Latitue = x.Latitue,
                 Longtitue = x.Longtitue,
                 Note = x.Note,
                 Province = x.Province,
                 UpdatedDate = DateTime.Now,
                 StaffNumm = x.StaffNumm,
                 StandardLevelCode = x.StandardLevelCode,
                 Status = x.Status,
                 StoreOwner = x.StoreOwner,
                 StorePhoneNumber = x.StorePhoneNumber,
                 Type = x.Type,
                 UpdatedBy = user,
                 Ward = x.Ward,
                 Updated = false,
            }).AsNoTracking().ToList();
        }
        public MOutletStoreUser GetOutletStoreUserById(long id)
        {
            return _context.MOutletStoreUser.FirstOrDefault(x => x.Id == id);
        }
        public MOutletStoreUser UpdateOutletStoreUser(MOutletStoreUser entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Updated = true;
            var rs = _context.MOutletStoreUser.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public List<MOutletStoreUser> GetAllOutletStoreUser(string user)
        {
            return _context.MOutletStoreUser.Where(x=> x.CreatedBy.Equals(user)).ToList();
        }
        public List<MOutletStoreUser> GetAllOutletStoreUser()
        {
            return _context.MOutletStoreUser.OrderBy(x=> x.UpdatedDate).ToList();
        }
        public List<MOutletStoreUser> GetAllOutletStoreUserByUpdate()
        {
            return _context.MOutletStoreUser.Where(x => x.Updated==true).ToList();
        }
        #endregion
        #region ProjectDetail
        public List<MProjectDetail> GetAllProjectDetail()
        {
            return _context.MProjectDetail.ToList();
        }
        public MProjectDetail DeleteProjectDetail(MProjectDetail entity)
        {
            var rs = _context.MProjectDetail.Remove(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MProjectDetail GetProjectDetail(long id)
        {
            return _context.MProjectDetail.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public MProjectDetail InsertProjectDetail(MProjectDetail entity)
        {
            var rs = _context.MProjectDetail.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public void UpdateListProjectDetail(List<MProjectDetail> entity)
        {
            _context.MProjectDetail.UpdateRange(entity);
            _context.SaveChanges();
        }
        public void UpdateProjectDetail(MProjectDetail entity)
        {
            _context.MProjectDetail.Update(entity);
            _context.SaveChanges();
        }
        public List<MProjectDetail> GetProjectDetailById(int id)
        {
            return _context.MProjectDetail.AsNoTracking().Where(x => x.ProjectId == id).ToList();
        }
        public List<MProjectDetail> GetProjectDetailByIdNotMediGroup(int id)
        {
            return _context.MProjectDetail.AsNoTracking().Where(x => x.ProjectId == id&&string.IsNullOrEmpty(x.MediGroupCode)).ToList();
        }
        public List<MProjectDetail> GetAllProjectDetailMediGroup()
        {
            return _context.MProjectDetail.Include(x=>x.Project).AsNoTracking().Where(x =>!string.IsNullOrEmpty(x.MediGroupCode)).ToList();
        }
        public List<MProjectDetail> GetAllProjectDetailMediGroupbyDrug(string medigroupCode)
        {
            return _context.MProjectDetail.Include(x => x.Project).AsNoTracking()
                .Where(x => !string.IsNullOrEmpty(x.MediGroupCode) && x.MediGroupCode.Equals(medigroupCode)).ToList();
        }
        public List<MProjectDetail> MapProjectDetail(List<MProjectDetail> detail)
        {
           // var drug = _context.MOutletStore.GroupBy(x => new { x.DrugStoreAddress, x.Area, x.District, x.DrugStoreName, x.Province, x.Ward}).Select(x => x.First()).ToList();
            var projectDetail = (from pd in detail
                                 join d in _context.MOutletStore on new { DrugName = StringUtils.ReplaceDataOutlet(pd.DrugName), Address = pd.Address, District = pd.District, Province = pd.Province, Area = pd.Area } equals new { DrugName = StringUtils.ReplaceDataOutlet(d.DrugStoreName), Address = d.DrugStoreAddress, District = d.District, Province = d.Province, Area = d.Area }
                                 into Group
                                 from j in Group.DefaultIfEmpty()
                                 select new MProjectDetail()
                                 {
                                     Id = pd.Id,
                                     Address = pd.Address,
                                     DrugName = pd.DrugName,
                                     District = pd.District,
                                     Component = pd.Component,
                                     Label = pd.Label,
                                     Position = pd.Position,
                                     Hsize = pd.Hsize,
                                     Vsize = pd.Vsize,
                                     Unit = pd.Unit,
                                     CostPayForDrugStore = pd.CostPayForDrugStore,
                                     CostPayForProduction = pd.CostPayForProduction,
                                     DateBeginRent = pd.DateBeginRent,
                                     CostPayForLp = pd.CostPayForLp,
                                     MonthRent = pd.MonthRent,
                                     Province = pd.Province,
                                     Area = pd.Area,
                                     MediGroupCode = j?.MediGroupCode,
                                     Ward = pd.Ward,
                                     ProjectId = pd.ProjectId,
                                     StoreOwner = j?.StoreOwner,
                                     StorePhoneNumber = j?.StorePhoneNumber,
                                     Images = pd.Images,
                                     NumOfArea = pd.NumOfArea
                                 }).ToList().Distinct().ToList();
            return projectDetail;
        }
        public void InsertDrugProjectDetail(List<MProjectDetail> projectDetail, List<MOutletStore> drug)
        {
            _context.MOutletStore.AddRange(drug);
            UpdateListProjectDetail(projectDetail);
        }
        public List<MProjectDetail> GetProjectDetailByMediGroup(string medigroup)
        {
            return _context.MProjectDetail.Include(x=>x.Project).Where(x => x.MediGroupCode.Equals(medigroup)).ToList();
        }
        #endregion

        #region Project
        public MProject DeleteProjectDrug(MProject entity)
        {
            var rs = _context.MProject.Remove(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MProject GetProjectDrug(int id)
        {
            return _context.MProject.FirstOrDefault(x => x.Id == id);
        }
        public MProject GetProjectInclude(int id)
        {
            return _context.MProject.Include(x=> x.MProjectDetail).FirstOrDefault(x => x.Id == id);
        }
        public MProject InsertProjectDrug(MProject entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsCompleted = false;
            entity.IsDeleted = false;
            //var d = entity.MProjectDetail;
            //entity.MProjectDetail = new List<MProjectDetail>();
            var rs = _context.MProject.Add(entity);
            //foreach (var item in d)
            //{
            //    item.ProjectId = rs.Entity.Id;
            //}
            //_context.MProjectDetail.AddRange(entity.MProjectDetail);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MProject UpdateProjectDrug(MProject entity)
        {
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MProject.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public List<MProjectCustomerModel> GetAllProject()
        {
            var rs = (from c in _context.Customer
                      join p in _context.MProject on c.Id equals p.CustomerId
                      select new MProjectCustomerModel()
                      {
                          Id = p.Id,
                          Name = p.Name,
                          ProjectType = p.ProjectType,
                          CustomerId = p.CustomerId,
                          ParentProjectId = p.ParentProjectId,
                          ParentProjectTypeId = p.ParentProjectTypeId,
                          CreatedBy = p.CreatedBy,
                          CreatedDate = p.CreatedDate,
                          UpdatedBy = p.UpdatedBy,
                          UpdatedDate = p.UpdatedDate,
                          IsCompleted = p.IsCompleted,
                          IsDeleted = p.IsDeleted,
                          Slug = p.Slug,
                          CustomerName = c.Name,
                          CustomerType = c.Type,
                          CustomerPhone = c.Phone,
                          CustomerEmail = c.Email
                      }).ToList();
            return rs;
        }
        public MProject ProjectMediGroupCode(MProject project, List<MProjectDetail> detail)
        {
            var projectDetail = MapProjectDetail(detail);
            //_context.MProjectDetail
            if (project?.Id !=0)
            {
                _context.MProjectDetail.RemoveRange(project.MProjectDetail);
                project.MProjectDetail = projectDetail;
                foreach (var item in project.MProjectDetail)
                {
                    item.ProjectId = project.Id;
                }
                return UpdateProjectDrug(project);
            }
            project.MProjectDetail = projectDetail;
            return InsertProjectDrug(project);
        }
        #endregion

        #region Component
        public MComponent DeleteComponent(int id)
        {
            var rs = GetComponentById(id);
            rs.IsDeleted = true;
            _context.SaveChanges();
            return rs;
        }
        public MComponent GetComponentById(int id)
        {
            return _context.MComponent.FirstOrDefault(x => x.Id == id);
        }
        public MComponent InsertComponent(MComponent entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = false;
            var rs = _context.MComponent.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MComponent UpdateComponent(MComponent entity)
        {
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MComponent.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public List<MComponent> GetAllComponent()
        {
            return _context.MComponent.ToList();
        }
        #endregion

        #region DrugLabel
        public MDrugLabel DeleteDrugLabel(MDrugLabel entity)
        {
            var rs = _context.MDrugLabel.Remove(entity);
            _context.SaveChanges();
            return rs.Entity;
        }
        public MDrugLabel GetDrugLabelById(int id)
        {
            return _context.MDrugLabel.FirstOrDefault(x => x.Id == id);
        }

        public MDrugLabel InsertDrugLabel(MDrugLabel entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MDrugLabel.Add(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public MDrugLabel UpdateDrugLabel(MDrugLabel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            var rs = _context.MDrugLabel.Update(entity);
            _context.SaveChanges();
            return rs.Entity;
        }

        public List<MDrugLabel> GetAllDrugLabel()
        {
            return _context.MDrugLabel.ToList();
        }
        #endregion


    }
}
