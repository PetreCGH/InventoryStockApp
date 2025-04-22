using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.EFCore;
using InventoryStockApp.Module.BusinessObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryStockApp.Module.Controllers
{
    public class ReportFilterController : ViewController<DetailView>
    {
        public ReportFilterController()
        {
            TargetObjectType = typeof(ReportFilter);
            TargetViewType = ViewType.DetailView;

            var acceptAction = new SimpleAction(this, "AcceptReportUnified", PredefinedCategory.View)
            {
                Caption = "Accept",
                ImageName = "Action_Grant"
            };
            acceptAction.Execute += AcceptAction_Execute;
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var filter = View.CurrentObject as ReportFilter;

            if (filter == null)
                return;

            if (filter.IsEntryReport)
            {
                GenerateEntryReport(filter, e);
            }
            else
            {
                GenerateExitReport(filter, e);
            }
        }

        private void GenerateEntryReport(ReportFilter filter, SimpleActionExecuteEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace(typeof(EntryReportResult));
            var context = (objectSpace as EFCoreObjectSpace)?.DbContext as InventoryStockAppEFCoreDbContext;

            int? warehouseId = filter.AllWarehouses ? null : filter.SelectedWarehouse?.Id;

            var result = context.Set<EntryReportResult>()
                .FromSqlInterpolated($@"
            EXEC GetEntryReport 
                @StartDate = {filter.StartDate}, 
                @EndDate = {filter.EndDate}, 
                @WarehouseId = {warehouseId}")
                .AsNoTracking()
                .ToList();

            var nonPersistentSpace = Application.CreateObjectSpace(typeof(EntryReportResult));
            foreach (var item in result)
            {
                nonPersistentSpace.GetObject(item);
            }

            var listView = Application.CreateListView(nonPersistentSpace, typeof(EntryReportResult), true);
            e.ShowViewParameters.CreatedView = listView;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        }

        private void GenerateExitReport(ReportFilter filter, SimpleActionExecuteEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace(typeof(ExitReportResult));
            var context = (objectSpace as EFCoreObjectSpace)?.DbContext as InventoryStockAppEFCoreDbContext;

            int? warehouseId = filter.AllWarehouses ? null : filter.SelectedWarehouse?.Id;

            var result = context.Set<ExitReportResult>()
                .FromSqlInterpolated($@"
            EXEC GetExitReport 
                @StartDate = {filter.StartDate}, 
                @EndDate = {filter.EndDate}, 
                @WarehouseId = {warehouseId}")
                .AsNoTracking()
                .ToList();

            var nonPersistentSpace = Application.CreateObjectSpace(typeof(ExitReportResult));
            foreach (var item in result)
            {
                nonPersistentSpace.GetObject(item);
            }

            var listView = Application.CreateListView(nonPersistentSpace, typeof(ExitReportResult), true);
            e.ShowViewParameters.CreatedView = listView;
            e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        }
    }
}