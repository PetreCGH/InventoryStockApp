using DevExpress.ExpressApp;
using InventoryStockApp.Module.BusinessObjects;
using System.Linq;

namespace InventoryStockApp.Module.Controllers
{
    public class ExitViewController : ObjectViewController<DetailView, Exit>
    {
        protected override void OnActivated()
        {
            base.OnActivated();

            var exit = View.CurrentObject as Exit;
            if (exit != null && exit.Number == 0)
            {
                var maxNumber = ObjectSpace.GetObjects<Exit>().Max(x => (int?)x.Number) ?? 0;
                exit.Number = maxNumber + 1;
            }
        }
    }
}