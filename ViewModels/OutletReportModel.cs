using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.ViewModels
{
    public class OutletReportModel
    {
        public long Id { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string MediGroupCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StoreOwner { get; set; }
        public string BankAccount { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Status { get; set; }
        public string StorePhoneNumber { get; set; }
        public int? StaffNumm { get; set; }
        public string Note { get; set; }
        public string Images { get; set; }
        public decimal? UnitCost { get; set; }
        public string Ward { get; set; }
        public string Type { get; set; }
        public string Label { get; set; }
        public string Position { get; set; }
        public double NumOfArea { get; set; }
        public string Unit { get; set; }
        public decimal CostPayForDrugStore { get; set; }
        public decimal CostPayForProduction { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Component { get; set; }
        public DateTime DateBeginRent { get; set; }
        public int MonthRent { get; set; }
        public string DrugName { get; set; }
        public string Address { get; set; }
    }
    public class ReportModel
    {
        public string District { get; set; }
        public string Province { get; set; }
        public string Position { get; set; }
        public DateTime DateBeginRent { get; set; }
        public int CustomerId { get; set; }
        public string Type { get; set; }
        public decimal CostPayForDrugStore { get; set; }
        public string Label { get; set; }
        public string Component { get; set; }
    }
}
