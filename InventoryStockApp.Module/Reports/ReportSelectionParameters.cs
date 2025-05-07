using System;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.EF;
using InventoryStockApp.Module.BusinessObjects;

namespace InventoryStockApp.Module.Reports
{
    public enum ReportType
    {
        EntryReport,
        ExitReport
    }

    [DefaultClassOptions]
    public class ReportSelectionParameters : BaseObject
    {
        public ReportSelectionParameters() : base() { }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Report Type")]
        public virtual ReportType? ReportType { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DateTime StartDate { get; set; } = new(DateTime.Today.Year, DateTime.Today.Month, 1);

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DateTime EndDate { get; set; } = DateTime.Today;

        public virtual bool AllWarehouses { get; set; } = true;

        public virtual Warehouse Warehouse { get; set; }
    }
}