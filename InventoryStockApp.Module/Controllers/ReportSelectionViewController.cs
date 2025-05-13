using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EFCore;
using InventoryStockApp.Module.Reports;




namespace InventoryStockApp.Module.Controllers
{
    public class ReportSelectionViewController : ObjectViewController<DetailView, ReportSelectionParameters>
    {
        public ReportSelectionViewController()
        {
            SimpleAction acceptAction = new SimpleAction(this, "AcceptReportParameters", PredefinedCategory.Edit)
            {
                Caption = "Accept",
                ImageName = "Action_Grant"
            };
            acceptAction.Execute += AcceptAction_Execute;

            SimpleAction listReportAction = new SimpleAction(this, "GenerateReportPreview", PredefinedCategory.Print)
            {
                Caption = "Listează",
                ImageName = "Action_Printing_Print"
            };
            listReportAction.Execute += ListReportAction_Execute;
        }

        private void AcceptAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var parameters = View.CurrentObject as ReportSelectionParameters;
            if (parameters == null)
            {
                throw new UserFriendlyException("Please complete all required fields.");
            }

            string viewTitle = View.Caption?.ToLowerInvariant() ?? "";
            string reportName = viewTitle.Contains("iesiri") ? "Exit Report" : "Entry Report";

            
            var reportData = ObjectSpace.GetObjects<ReportDataV2>().FirstOrDefault(r => r.DisplayName == reportName);
            if (reportData == null)
            {
                throw new UserFriendlyException($"Report '{reportName}' not found.");
            }

            if (reportData.Content == null)
            {
                throw new UserFriendlyException($"Report '{reportName}' has no content. Please recreate it.");
            }

            
            var report = ReportDataProvider.ReportsStorage.LoadReport(reportData);

            if (report is EntryReport entryReport)
            {

                
                entryReport.SetSqlConnection("DESKTOP-2VG93U0", "InventoryStockAp");

                entryReport.Parameters["StartDate"].Value = parameters.StartDate;
                entryReport.Parameters["EndDate"].Value = parameters.EndDate;
                entryReport.Parameters["WarehouseId"].Value = parameters.AllWarehouses ? 0 : parameters.Warehouse?.Id ?? 0;

                entryReport.RequestParameters = false;
            }

            else if (report is ExitReport exitReport)
            {
                exitReport.SetSqlConnection("DESKTOP-2VG93U0", "InventoryStockAp");

                exitReport.Parameters["StartDate"].Value = parameters.StartDate;
                exitReport.Parameters["EndDate"].Value = parameters.EndDate;
                exitReport.Parameters["WarehouseId"].Value = parameters.AllWarehouses ? 0 : parameters.Warehouse?.Id ?? 0;



                exitReport.RequestParameters = false;
            }

            var controller = Frame.GetController<ReportServiceController>();
            var handle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
            controller?.ShowPreview(handle);
        }

        private void ListReportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AcceptAction_Execute(sender, e); 
        }

        private CriteriaOperator ParametersToCriteria(Dictionary<string, object> parameters)
        {
            var conditions = parameters
                .Select(p => new BinaryOperator(p.Key, p.Value ?? DBNull.Value))
                .Cast<CriteriaOperator>()
                .ToList();

            return new GroupOperator(GroupOperatorType.And, conditions);
        }


        
    }
}