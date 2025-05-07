using System;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace InventoryStockApp.Module.Reports
{
    [VisibleInReports]
    public partial class EntryReport : XtraReport
    {
        public EntryReport()
        {
            InitializeComponent();

            // 1. Definire parametri vizibili în UI
            Parameter startDateParam = new Parameter
            {
                Name = "StartDate",
                Type = typeof(DateTime),
                Value = DateTime.Today.AddMonths(-1),
                Description = "Data început",
                Visible = true
            };

            Parameter endDateParam = new Parameter
            {
                Name = "EndDate",
                Type = typeof(DateTime),
                Value = DateTime.Today,
                Description = "Data sfârșit",
                Visible = true
            };

            Parameter warehouseIdParam = new Parameter
            {
                Name = "WarehouseId",
                Type = typeof(int),
                Value = 0, // 0 = toate gestiunile
                Description = "Gestiune ID",
                Visible = true
            };

            // 2. Adăugare parametri la raport
            this.Parameters.AddRange(new Parameter[]
            {
                startDateParam,
                endDateParam,
                warehouseIdParam
            });

            // 3. Asociere parametri cu SQL Query
            this.sqlDataSource1.Queries[0].Parameters[0].Value =
                new DevExpress.DataAccess.Expression("[Parameters.StartDate]", typeof(DateTime));

            this.sqlDataSource1.Queries[0].Parameters[1].Value =
                new DevExpress.DataAccess.Expression("[Parameters.EndDate]", typeof(DateTime));

            this.sqlDataSource1.Queries[0].Parameters[2].Value =
                new DevExpress.DataAccess.Expression("[Parameters.WarehouseId]", typeof(int));

            // 4. Configurare sursă de date
            this.sqlDataSource1.ConnectionName = "ConnectionString"; 
            this.DataSource = this.sqlDataSource1;
            this.DataMember = "GetEntryReport";
        }

        public void SetSqlConnection(string server, string database)
        {
            var connectionParams = new DevExpress.DataAccess.ConnectionParameters.MsSqlConnectionParameters(
                serverName: server,
                databaseName: database,
                userName: null,
                password: null,
                authorizationType: DevExpress.DataAccess.ConnectionParameters.MsSqlAuthorizationType.Windows
            );

            this.sqlDataSource1.ConnectionParameters = connectionParams;
            this.sqlDataSource1.RebuildResultSchema(); // Actualizează schema dacă e nevoie
        }
    }
}