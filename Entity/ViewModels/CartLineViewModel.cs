using KantindenAl.App.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
	public class CartLineViewModel
	{
        public int Id { get; set; }
        public int Quantity { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Product Product { get; set; }
    }
}
