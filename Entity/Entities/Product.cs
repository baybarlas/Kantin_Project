using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
	public class Product : BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }    
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsPopuler { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }

        
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        public virtual Category Category { get; set; }
	}
}
