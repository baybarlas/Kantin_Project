using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Entities
{
	public class Student : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StdNo { get; set; }
        public string ClassName { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }

        public virtual School School { get; set;}
    }
}
