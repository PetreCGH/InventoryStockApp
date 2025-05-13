using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.UI;
using InventoryStockApp.Module.Reports;

namespace InventoryStockApp.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ViewController.
    public partial class ReportDataV2ViewController : ViewController
    {
        private IReportDataSourceHelper reportDataSourceHelper;
        public ReportDataV2ViewController()
        {
            InitializeComponent();
            this.TargetViewId = "";
            this.TargetObjectType = typeof(ReportDataV2);
            this.TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Frame.Application.Modules);
            reportDataSourceHelper = reportsModule.ReportsDataSourceHelper;

            reportDataSourceHelper.BeforeShowPreview += ReportDataSourceHelper_BeforeShowPreview;
        }

        private void ReportDataSourceHelper_BeforeShowPreview(object sender, BeforeShowPreviewEventArgs e)
        {

            XtraReport report = e.Report as XtraReport;

            switch (report.GetType().Name)
            {

                case nameof(EntryReport):

                    var rpo = report.Parameters["XafReportParametersObject"].Value as ReportSelectionParameters;

                    if (report.Parameters["StartDate"] != null)
                        report.Parameters["StartDate"].Value = rpo.StartDate;

                    if (report.Parameters["EndDate"] != null)
                        report.Parameters["EndDate"].Value = rpo.EndDate;

                    if (report.Parameters["WarehouseId"] != null)
                    {
                        if (rpo.AllWarehouses)
                        {
                            report.Parameters["WarehouseId"].Value = 0;
                        }
                        else
                        {
                            report.Parameters["WarehouseId"].Value = rpo.Warehouse.Id;
                        }
                    }


                    break;

            }

        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {

            reportDataSourceHelper.BeforeShowPreview -= ReportDataSourceHelper_BeforeShowPreview;

            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
