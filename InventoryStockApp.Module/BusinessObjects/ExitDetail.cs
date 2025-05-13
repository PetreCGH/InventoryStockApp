using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    public class ExitDetail : IObjectSpaceLink
    {
        [Browsable(false)]
        [NotMapped]
        public IObjectSpace ObjectSpace { get; set; }

        [Key]
        public virtual int Id { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Exit Exit { get; set; }

        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("AvailableProducts")]
        
        public virtual Product Product { get; set; }

        [RuleRange(DefaultContexts.Save, 1, int.MaxValue, CustomMessageTemplate = "Quantity must be at least 1.")]
        public virtual int Quantity { get; set; }

        [NotMapped]
        public decimal Value => Product?.UnitPrice * Quantity ?? 0;

        [Browsable(false)]
        [NotMapped]
        public IList<Product> AvailableProducts
        {
            get
            {
                if (Exit?.Warehouse == null)
                    return new List<Product>();

                
                var objectSpace = ((DevExpress.ExpressApp.IObjectSpaceLink)this).ObjectSpace;

                
                var entries = objectSpace.GetObjects<Entry>()
                    .Where(e => e.Warehouse.Id == Exit.Warehouse.Id)
                    .ToList();

                
                return entries
                    .SelectMany(e => e.Details)
                    .Select(d => d.Product)
                    .Distinct()
                    .ToList();
                
            }
        }
    }
}