using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base;
using InventoryStockApp.Module.BusinessObjects;

namespace InventoryStockApp.Blazor.Server.Controllers
{
    public class ReportPrintController : ViewController<DetailView>
    {
        public ReportPrintController()
        {
            TargetObjectType = typeof(ReportFilter);
            TargetViewType = ViewType.DetailView;

            var printAction = new SimpleAction(this, "PrintReport", PredefinedCategory.View)
            {
                Caption = "Print",
                ImageName = "Action_Printing_Print"
            };
            printAction.Execute += PrintAction_Execute;
        }

        private void PrintAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var filter = View.CurrentObject as ReportFilter;
            if (filter == null)
                return;

            
            var reportName = filter.IsEntryReport ? "Entry Report" : "Exit Report";

            
            var reportService = Frame.GetController<ReportServiceController>();

            
            var criteria = CriteriaOperator.Parse(
                "Date >= ? And Date <= ? And (Warehouse.Id = ? Or ?)",
                filter.StartDate,
                filter.EndDate,
                filter.SelectedWarehouse?.Id,
                filter.AllWarehouses
            );

            
            reportService?.ShowPreview(reportName, criteria);
        }
    }
}
