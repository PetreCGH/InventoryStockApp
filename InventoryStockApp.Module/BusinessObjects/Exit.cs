using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel;


namespace InventoryStockApp.Module.BusinessObjects  
{
    [RuleCriteria(
    "Exit.MustHaveAtLeastOneDetailAfterSave",
    DefaultContexts.Save,
    "Details.Count > 0",
    CustomMessageTemplate = "You must add at least one product to the exit.",
    TargetCriteria = "IsNewObject=false")]
    [DefaultClassOptions]
    public class Exit
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int Number { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DateTime Date { get; set; } = DateTime.Now;

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Partner Partner { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Warehouse Warehouse { get; set; }

        [Aggregated]
        public virtual ObservableCollection<ExitDetail> Details { get; set; } = new();

        [Browsable(false)]

        //    [RuleFromBoolProperty(nameof(HasExitDetails), DefaultContexts.Save,
        //CustomMessageTemplate = "You must add at least one product to the exit.")]
        public bool HasExitDetails => Details != null && Details.Any();

        [NotMapped]
        [ModelDefault("AllowEdit", "False")]
        public decimal TotalValue => Details?.Sum(d => d.Quantity * d.Product.UnitPrice) ?? 0;
    }
}