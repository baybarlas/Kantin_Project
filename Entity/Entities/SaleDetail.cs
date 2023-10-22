using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
    public class SaleDetail : BaseEntity
    {
        public int SaleId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
    }
}
