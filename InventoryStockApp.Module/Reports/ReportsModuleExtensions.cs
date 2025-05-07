using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EFCore;
using DevExpress.Persistent.BaseImpl.EF;

namespace InventoryStockApp.Module.Reports
{
    public static class ReportsModuleExtensions
    {
        public static void CreateReportDataV2<T>(this ReportsModuleV2 module, IObjectSpace objectSpace, string reportDisplayName)
        {
            var reportData = module.ReportDataType == null
                ? null
                : objectSpace.CreateObject(module.ReportDataType) as ReportDataV2;

            if (reportData != null)
            {
                reportData.DisplayName = reportDisplayName;
                reportData.IsInplaceReport = true;
                reportData.DataTypeName = typeof(T).FullName;
            }
        }
    }
}