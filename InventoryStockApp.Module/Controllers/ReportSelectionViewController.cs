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


            // 1. Verificăm dacă obiectul curent este valid
            var parameters = View.CurrentObject as ReportSelectionParameters;
            if (parameters == null || parameters.ReportType == null)
            {
                throw new UserFriendlyException("Please complete all required fields.");
            }

            

            // 2. Determinăm numele raportului în funcție de tipul selectat
            string reportName = parameters.ReportType == InventoryStockApp.Module.Reports.ReportType.EntryReport
                ? "Entry Report"
                : "Exit Report";

            // 3. Căutăm raportul salvat în baza de date
            var reportData = ObjectSpace.GetObjects<ReportDataV2>().FirstOrDefault(r => r.DisplayName == reportName);
            if (reportData == null)
            {
                throw new UserFriendlyException($"Report '{reportName}' not found.");
            }

            if (reportData.Content == null)
            {
                throw new UserFriendlyException($"Report '{reportName}' has no content. Please recreate it.");
            }

            // 4. Încărcăm raportul fizic (clasa EntryReport), nu doar metadatele
            var report = ReportDataProvider.ReportsStorage.LoadReport(reportData);

            // 5. Aplicăm parametrii și conexiunea DOAR dacă raportul este EntryReport
            if (report is EntryReport entryReport)
            {

                // Setăm conexiunea SQL la raport
                entryReport.SetSqlConnection("DESKTOP-2VG93U0", "InventoryStockAp");

                // Setăm valorile parametrilor
                entryReport.Parameters["StartDate"].Value = parameters.StartDate;
                entryReport.Parameters["EndDate"].Value = parameters.EndDate;
                entryReport.Parameters["WarehouseId"].Value = parameters.AllWarehouses ? 0 : parameters.Warehouse?.Id ?? 0;

                


                // Oprim cererea de parametri la runtime
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

            // 6. Afișăm raportul în preview-ul DevExpress
            var controller = Frame.GetController<ReportServiceController>();
            var handle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
            controller?.ShowPreview(handle);
        }

        private void ListReportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AcceptAction_Execute(sender, e); // same behavior
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