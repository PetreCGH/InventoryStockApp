using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Entry
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

        [ImmediatePostData]
        [Aggregated]
        
        public virtual ObservableCollection<EntryDetail> Details { get; set; } = new();

        [NotMapped]
        [ModelDefault("AllowEdit", "False")]
        public decimal TotalValue => Details?.Sum(d => d.Quantity * d.Product.UnitPrice) ?? 0;
    }
}