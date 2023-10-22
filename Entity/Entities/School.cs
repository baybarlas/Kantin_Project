using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
	public class School : BaseEntity
	{
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; } = string.Empty;

        public virtual List<Student> Students { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
