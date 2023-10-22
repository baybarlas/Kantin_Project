using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
	public interface IAccountService
	{
		Task<string> CreateUserAsync(RegisterViewModel model);

		Task<string> CreateOwnerAsync(OwnerRegisterViewModel model, string schoolId);

		Task<string> CreateRoleAsync(RoleViewModel model);

		Task<string> EditUserAsync(UserViewModel model);

		Task<List<RoleViewModel>> GetAllRolesAsync();

		Task<List<UserViewModel>> GetAllUsersAsync();

		Task<List<UserViewModel>> GetAllOwnersAsync();

		Task<string> FindUserByNameAsync(LoginViewModel model);
		Task<UserViewModel> FindUserByUserNameAsync(string userName);

		Task<UserViewModel> FindUserById(string id);

		Task LogoutAsync();

		Task<RoleViewModel> FindRoleById(string id);

        Task<UserInOutViewModel> GetAllUsersWithRole(string id);

		Task<string> EditRoleListAsync(EditRoleViewModel model);

		OwnerViewModel FindUserBySchoolId(string schoolId);
		
		Task UpdateUser(UserViewModel model);
		Task UpdateOwner(OwnerViewModel model);
        
    }
}
