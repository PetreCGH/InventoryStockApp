using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using InventoryStockApp.Module.BusinessObjects;
using DevExpress.ExpressApp.SystemModule;

namespace InventoryStockApp.Module.Controllers
{
    public class WarehouseViewController : ObjectViewController<DetailView, Warehouse>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var warehouse = View.CurrentObject as Warehouse;
            if (warehouse != null && warehouse.Code == 0)
            {
                var max = ObjectSpace.GetObjects<Warehouse>().Max(w => (int?)w.Code) ?? 0;
                warehouse.Code = max + 1;
            }
        }
    }
}
