using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.ViewModels
{
    public class UserInOutViewModel
    {
        public RoleViewModel Role { get; set; }
        public List<UserViewModel> UsersInRole { get; set; }
        public List<UserViewModel> UsersOutRole { get; set; }
    }
}
