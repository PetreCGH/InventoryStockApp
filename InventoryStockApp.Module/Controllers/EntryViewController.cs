using DevExpress.ExpressApp;
using InventoryStockApp.Module.BusinessObjects;
using System.Linq;

namespace InventoryStockApp.Module.Controllers
{
    public class EntryViewController : ObjectViewController<DetailView, Entry>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var entry = View.CurrentObject as Entry;
            if (entry != null && entry.Number == 0)
            {
                var maxNumber = ObjectSpace.GetObjects<Entry>().Max(x => (int?)x.Number) ?? 0;
                entry.Number = maxNumber + 1;
            }
        }
    }
}
