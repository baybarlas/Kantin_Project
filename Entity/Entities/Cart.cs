using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
	public class Cart : BaseEntity
	{
        public string? ReceiptNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public int UserId { get; set; }
        public int? SchoolId { get; set; }

        public virtual List<CartLine> CartLines { get; set; }

    }
}
