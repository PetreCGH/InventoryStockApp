using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EFCore;
using InventoryStockApp.Module;
using InventoryStockApp.Module.Reports;

namespace InventoryStockApp.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891/core-prerequisites-for-design-time-model-editor-with-entity-framework-core-data-model.
public class InventoryStockAppContextInitializer : DbContextTypesInfoInitializerBase
{
    protected override DbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryStockAppEFCoreDbContext>()
            .UseSqlServer(";") // You should configure your database connection string here.
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new InventoryStockAppEFCoreDbContext(optionsBuilder.Options);
    }
}

//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class InventoryStockAppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventoryStockAppEFCoreDbContext>
{
    public InventoryStockAppEFCoreDbContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<InventoryStockAppEFCoreDbContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-2VG93U0;Database=InventoryStockAp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new InventoryStockAppEFCoreDbContext(optionsBuilder.Options);
    }
}

[TypesInfoInitializer(typeof(InventoryStockAppContextInitializer))]
public class InventoryStockAppEFCoreDbContext : DbContext
{
    public InventoryStockAppEFCoreDbContext(DbContextOptions<InventoryStockAppEFCoreDbContext> options) : base(options)
    {
    }

    // DbSets for entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryDetail> EntryDetails { get; set; }
    public DbSet<Exit> Exits { get; set; }
    public DbSet<ExitDetail> ExitDetails { get; set; }
    public DbSet<ReportDataV2> Reports { get; set; }


    // Override OnModelCreating to configure entity models
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReportDataV2>().HasData(new ReportDataV2[]
        {
             new ReportDataV2
             {
                 ID = Guid.Parse("108d9487-846a-4fd3-9c83-c1cdfb72aa04"),
                 DisplayName = "Entry Report",
                 PredefinedReportTypeName = typeof(EntryReport).ToString(),
                 IsInplaceReport = false,
                 DataTypeName = "",
                 ParametersObjectTypeName = typeof(ReportSelectionParameters).ToString()
             },
             new ReportDataV2
             {
                 ID = Guid.Parse("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"),
                 DisplayName = "Exit Report",
                 PredefinedReportTypeName =typeof(ExitReport).ToString(),
                 IsInplaceReport = false,
                 DataTypeName = "",
                 ParametersObjectTypeName = typeof(ReportSelectionParameters).ToString()
             }

        });

        // Add precision and scale for decimal fields




        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Entry>()
    .HasMany(e => e.Details)
    .WithOne(d => d.Entry)
    .OnDelete(DeleteBehavior.Cascade);

        // Further model configurations
        modelBuilder.UseDeferredDeletion(this);
        modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.SetNull, DeleteBehavior.Cascade);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
    }
}