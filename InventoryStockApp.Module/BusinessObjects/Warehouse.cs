using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Master Data")]
    public class Warehouse
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int Code { get; set;}

        public virtual string Name { get; set; }
      
        
    }
}
