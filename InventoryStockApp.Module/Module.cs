using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using InventoryStockApp.Module.BusinessObjects;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl.EFCore;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.Persistent.Validation;
using InventoryStockApp.Module.Reports;



namespace InventoryStockApp.Module;

public sealed class InventoryStockAppModule : ModuleBase
{
    
    private XafApplication application;

    public InventoryStockAppModule()
    {
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
        RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));

        AdditionalExportedTypes.Add(typeof(Product));
        AdditionalExportedTypes.Add(typeof(Partner));
        AdditionalExportedTypes.Add(typeof(Warehouse));
        AdditionalExportedTypes.Add(typeof(ReportDataV2));
        AdditionalExportedTypes.Add(typeof(Entry));
        AdditionalExportedTypes.Add(typeof(EntryDetail));
        AdditionalExportedTypes.Add(typeof(EntryReport));
        AdditionalExportedTypes.Add(typeof(InventoryStockApp.Module.Reports.ReportSelectionParameters));



    }

    [assembly: RuleCriteria("MustHaveEntryDetails", DefaultContexts.Save, TargetType = typeof(Entry),
    TargetCriteria = "Details.Count > 0",
    CustomMessageTemplate = "You must add at least one product to the entry.")]

    public override void Setup(XafApplication application)
    {
        base.Setup(application);
        this.application = application;

        ReportsModuleV2 reportsModule = application.Modules.FindModule<ReportsModuleV2>();
        if (reportsModule != null)
        {
            reportsModule.EnableInplaceReports = true;
            reportsModule.ReportDataType = typeof(ReportDataV2);

        }
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
        
        return new ModuleUpdater[] {
            new DatabaseUpdate.Updater(objectSpace, versionFromDB, application)
        };
    }
}