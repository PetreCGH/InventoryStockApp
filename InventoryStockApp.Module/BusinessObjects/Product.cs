using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;



namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Master Data")]
    public class Product
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual int Code { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public virtual string Name { get; set; }

        [ModelDefault("DisplayFormat", "{0:N2}")]
        public virtual decimal UnitPrice { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
        
}
