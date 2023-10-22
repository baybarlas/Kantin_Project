using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
    public class WalletActivity : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NewBalance { get; set; }
        public string? receiptNo { get; set; }
    }
}
