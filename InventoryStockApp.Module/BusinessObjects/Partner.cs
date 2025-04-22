using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryStockApp.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Partner
    {
        [Key]
        public virtual int Id { get; set; }
        
        public virtual int Code { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public virtual string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public virtual PartnerType Type { get; set; }

        public virtual string CUI { get; set; }

        public virtual string Address { get; set; }

      

    }
}
