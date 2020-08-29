using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Entities;

namespace LienPhatERP.ViewModels
{
    public class MProjectDetailModel
    {
        public long Id { get; set; }
        public string Component { get; set; }
        public string Label { get; set; }
        public string Position { get; set; }
        public double Hsize { get; set; }
        public double Vsize { get; set; }
        public double NumOfArea { get; set; }
        public string Unit { get; set; }
        public decimal CostPayForDrugStore { get; set; }
        public decimal CostPayForProduction { get; set; }
        public DateTime DateBeginRent { get; set; }
        public int MonthRent { get; set; }
        public int ProjectId { get; set; }
        public string Note { get; set; }
        public string Images { get; set; }
        public string DrugName { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string MediGroupCode { get; set; }
        public string Ward { get; set; }

        public MProject Project { get; set; }
        public void Update(MProjectDetail rs)
        {
            rs.Component = Component;
            rs.Label = Label;
            rs.Position = Position;
            rs.Hsize = Hsize;
            rs.NumOfArea = NumOfArea;
            rs.Unit = Unit;
            rs.CostPayForDrugStore = CostPayForDrugStore;
            rs.CostPayForProduction = CostPayForProduction;
            rs.DateBeginRent = DateBeginRent;
            rs.MonthRent = MonthRent;
            rs.Note = Note;
            rs.DrugName = DrugName;
            rs.District = District;
            rs.Province = Province;
            rs.Area = Area;
            rs.Address = Address;
            rs.Ward = Ward;
        }
    }
}
