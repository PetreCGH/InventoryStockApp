using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DomainComponent]
    public class ExitReportResult
    {
        public virtual string Warehouse { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Product { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal Value => UnitPrice * Quantity;
    }
}
