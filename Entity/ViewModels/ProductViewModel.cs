using KantindenAl.App.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
	public class ProductViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public decimal UnitPrice { get; set; }
        public bool IsPopuler { get; set; }
        public bool IsDeleted { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
		public string? ImageUrl { get; set; }
        public int OwnerId { get; set; }
        public int SchoolId { get; set; }
    }
}
