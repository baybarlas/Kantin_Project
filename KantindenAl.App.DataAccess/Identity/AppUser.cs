using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.DataAccess.Identity
{
	public class AppUser : IdentityUser<int>
	{
        public string FirstName { get; set; }
        public string? MiddleName { get; set; } 
        public string LastName { get; set; }
        public decimal Balance { get; set; } = 0;
        public int? SchoolId { get; set; }
        public string? StoreName { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }

    }
}
