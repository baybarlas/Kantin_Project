using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ReceiptNo { get; set; }
        public int UserId { get; set; }
        public int OwnerId { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
