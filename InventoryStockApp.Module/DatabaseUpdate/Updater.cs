using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl.EF;
using InventoryStockApp.Module.Reports;
using DevExpress.XtraReports.UI;


namespace InventoryStockApp.Module.DatabaseUpdate;

public class Updater : ModuleUpdater
{
    private readonly XafApplication application;

    public Updater(IObjectSpace objectSpace, Version currentDBVersion, XafApplication application)
        : base(objectSpace, currentDBVersion)
    {
        this.application = application;
    }

    public override void UpdateDatabaseAfterUpdateSchema()
    {
        base.UpdateDatabaseAfterUpdateSchema();

        ReportsModuleV2 reportsModule = application.Modules.FindModule<ReportsModuleV2>();
        if (reportsModule != null)
        {
            CreateOrUpdateReport<EntryReport>("Entry Report");
            CreateOrUpdateReport<ExitReport>("Exit Report");
        }
    }

    private void CreateOrUpdateReport<TReport>(string displayName)
        where TReport : XtraReport, new()
    {
        var existingReport = ObjectSpace.FirstOrDefault<ReportDataV2>(
            r => r.DisplayName == displayName);

        if (existingReport != null && existingReport.Content == null)
        {
            ObjectSpace.Delete(existingReport);
            ObjectSpace.CommitChanges();
            existingReport = null;
        }

        if (existingReport == null)
        {
            var report = new TReport();

            var reportData = ObjectSpace.CreateObject<ReportDataV2>();
            reportData.DataTypeName = typeof(TReport).AssemblyQualifiedName;
            reportData.DisplayName = displayName;
            reportData.IsInplaceReport = true;


            using (var stream = new MemoryStream())
            {
                report.SaveLayoutToXml(stream);
                stream.Position = 0;

                using (var reader = new BinaryReader(stream))
                {
                    reportData.Content = reader.ReadBytes((int)stream.Length);
                }
            }

            ObjectSpace.CommitChanges();
        }
    }


    private void CreateReport<T>(string displayName) where T : DevExpress.XtraReports.UI.XtraReport
    {
        var reportData = ObjectSpace.FirstOrDefault<ReportDataV2>(r => r.DisplayName == displayName);
        if (reportData == null)
        {
            var reportModule = application.Modules.FindModule<ReportsModuleV2>();
            if (reportModule != null)
            {
                reportModule.CreateReportDataV2<T>(ObjectSpace, displayName);
            }
        }
    }

    public override void UpdateDatabaseBeforeUpdateSchema()
    {
        base.UpdateDatabaseBeforeUpdateSchema();
    }
}
