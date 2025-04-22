using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class EntryDetail
    {
        [Key]
        public virtual int Id { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Entry Entry { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Product Product { get; set; }

        [RuleRange(DefaultContexts.Save, 1, int.MaxValue, CustomMessageTemplate = "Quantity must be at least 1.")]
        public virtual decimal Quantity { get; set; }

        [NotMapped]
        public decimal Value => Product?.UnitPrice * Quantity ?? 0;
    }
}