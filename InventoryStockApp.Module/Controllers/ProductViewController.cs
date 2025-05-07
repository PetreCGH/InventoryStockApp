using DevExpress.ExpressApp;
using InventoryStockApp.Module.BusinessObjects;
using System.Linq;

namespace InventoryStockApp.Module.Controllers
{
    public class ProductViewController : ObjectViewController<DetailView, Product>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var product = View.CurrentObject as Product;
            if (product != null && product.Code == 0)
            {
                var maxCode = ObjectSpace.GetObjects<Product>().Max(p => (int?)p.Code) ?? 0;
                product.Code = maxCode + 1;
            }
        }
    }
}