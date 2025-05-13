using System;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.EF;
using InventoryStockApp.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;

namespace InventoryStockApp.Module.Reports
{
    public enum ReportType
    {
        EntryReport,
        ExitReport
    }

    [DomainComponent]
    public class ReportSelectionParameters : ReportParametersObjectBase
    {
        public ReportSelectionParameters(IObjectSpaceCreator provider) : base(provider)
        {
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Report Type")]
        public virtual ReportType? ReportType { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DateTime StartDate { get; set; } = new(DateTime.Today.Year, DateTime.Today.Month, 1);

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DateTime EndDate { get; set; } = DateTime.Today;

        public virtual bool AllWarehouses { get; set; } = true;


        //[Appearance("HideWarehouseIfAllSelected", Visibility = ViewItemVisibility.Hide, Criteria = "AllWarehouses = true", TargetItems = nameof(Warehouse))]
        public virtual Warehouse Warehouse { get; set; }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("1=0");
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override SortProperty[] GetSorting()
        {
            return Array.Empty<SortProperty>();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace<Entry>();
        }
    }
}