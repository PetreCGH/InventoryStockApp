using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using InventoryStockApp.Module.BusinessObjects;

namespace InventoryStockApp.Module.Controllers
{
    public class PartnerViewController : ObjectViewController<DetailView, Partner>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var partner = View.CurrentObject as Partner;
            if (partner != null && partner.Code == 0)
            {
                var maxCode = ObjectSpace.GetObjects<Partner>().Max(p => (int?)p.Code) ?? 0;
                partner.Code = maxCode + 1;
            }

        }
    }
}
