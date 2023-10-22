using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
    public class Sale : BaseEntity
    {
        public string ReceiptNo { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public decimal? TotalAmount { get; set; }

        public virtual List<SaleDetail> SaleDetails { get; set; }
    }
}
