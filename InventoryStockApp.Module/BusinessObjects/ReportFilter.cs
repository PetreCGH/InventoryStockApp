using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace InventoryStockApp.Module.BusinessObjects
{
    [DomainComponent]
    public class ReportFilter
    {
        [Required]
        public virtual bool IsEntryReport { get; set; } = true;

        [Required]
        public virtual DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30);

        [Required]
        public virtual DateTime EndDate { get; set; } = DateTime.Now;

        public virtual bool AllWarehouses { get; set; } = true;

        [Appearance("HideWarehouseSelector", TargetItems = nameof(SelectedWarehouse), Criteria = "AllWarehouses = true", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        public virtual Warehouse SelectedWarehouse { get; set; }
    }
}
