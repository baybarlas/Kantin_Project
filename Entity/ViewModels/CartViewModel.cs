using KantindenAl.App.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
	public class CartViewModel
	{
        public int Id { get; set; }
        public string ReceiptNo { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		public decimal TotalAmount { get; set; }
		public int? StoreId { get; set; }
		public int UserId { get; set; }
        public int? SchoolId { get; set; }

    }
}
