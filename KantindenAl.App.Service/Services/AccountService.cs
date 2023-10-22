using AutoMapper;
using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {

            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public async Task<string> CreateRoleAsync(RoleViewModel model)
        {
            string msg = string.Empty;
            var role = new AppRole()
            {
                Name = model.Name,
                Description = model.Description,
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                msg = "OK";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    msg = error.Description;
                }
            }
            return msg;
        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Parent");
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<string> CreateUserAsync(RegisterViewModel model)
        {
            string msg = string.Empty;
            var user = new AppUser()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.ConfirmPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Parent");
                msg = "OK";
                return msg;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    msg = error.Description;
                }
            }
            return msg;
        }

        public async Task<List<UserViewModel>> GetAllOwnersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Owner");
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<string> CreateOwnerAsync(OwnerRegisterViewModel model, string schoolId)
        {
            string msg = string.Empty;
            var user = new AppUser()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                UserName = model.Username,
                StoreName = model.StoreName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                SchoolId = Convert.ToInt32(schoolId)
            };
            var result = await _userManager.CreateAsync(user, model.ConfirmPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Owner");
                msg = "OK";
                return msg;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    msg = error.Description;
                }
            }
            return msg;
        }

        public async Task<string> FindUserByNameAsync(LoginViewModel model)
        {
            string msg = string.Empty;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                msg = "Kullanıcı bulunamadı.";
                return msg;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                msg = "OK";
            }
            return msg;
        }
        public async Task<UserViewModel> FindUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserViewModel> FindUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<RoleViewModel> FindRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return _mapper.Map<RoleViewModel>(role);
        }

        public async Task<UserInOutViewModel> GetAllUsersWithRole(string id)
        {
            var role = await this.FindRoleById(id);
            var usersInRole = new List<AppUser>();
            var usersOutRole = new List<AppUser>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    usersInRole.Add(user);  //Bu rolde bulunan kullanıcıların listesi
                }
                else
                {
                    usersOutRole.Add(user); //Bu rolde olmayan kullanıcıların listesi
                }
            }
            UserInOutViewModel model = new UserInOutViewModel()
            {
                Role = _mapper.Map<RoleViewModel>(role),
                UsersInRole = _mapper.Map<List<UserViewModel>>(usersInRole),
                UsersOutRole = _mapper.Map<List<UserViewModel>>(usersOutRole)
            };
            return model;
        }
        public async Task<string> EditRoleListAsync(EditRoleViewModel model)
        {
            string msg = "OK";
            foreach (var userId in model.UserIdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        msg = $"{user.UserName} role eklenemedi";

                    }
                }
            }
            foreach (var userId in model.UserIdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        msg = $"{user.UserName} rolden çıkarılamadı";
                    }
                }

            }
            return msg;
        }

        public async Task<string> EditUserAsync(UserViewModel model)
        {
            string msg = string.Empty;
            var user = await _userManager.FindByNameAsync(model.Username);

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.UserName = model.Username;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Balance = model.Balance;
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                msg = "OK";
                return msg;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    msg = error.Description;
                }
            }
            return msg;


        }

        public OwnerViewModel FindUserBySchoolId(string schoolId)
        {
            var user = _userManager.Users.Where(u => u.SchoolId == Convert.ToInt32(schoolId)).FirstOrDefault();
            return _mapper.Map<OwnerViewModel>(user);
        }

        public async Task UpdateUser(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.Balance = model.Balance;
            await _userManager.UpdateAsync(user);
            //await _unitOfWork.CommitAsync();
        }

        public async Task UpdateOwner(OwnerViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.Balance = model.Balance;
            await _userManager.UpdateAsync(user);
            //await _unitOfWork.CommitAsync();
        }
    }
}
