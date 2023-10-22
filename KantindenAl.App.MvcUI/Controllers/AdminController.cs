using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly ISchoolService _schoolService;


        public AdminController(IAccountService accountService, ISchoolService schoolService)
        {
            _accountService = accountService;
            _schoolService = schoolService;
        }

        public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> UserList()
		{
			var model = await _accountService.GetAllUsersAsync();
			return View(model);
		}

		public async Task<IActionResult> OwnerList()
		{
            var model = await _accountService.GetAllOwnersAsync();
            return View(model);
        }

		public async Task<IActionResult> Role()
		{
			var model = await _accountService.GetAllRolesAsync();
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> EditRole(string id)
		{
            var model = await _accountService.GetAllUsersWithRole(id);

            return View(model);
        }

		[HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            string msg = await _accountService.EditRoleListAsync(model);
            if (msg != "OK")
            {
                ModelState.AddModelError("", msg);
                return View(model);
            }

            return RedirectToAction("EditRole", "Admin", new { Id = model.RoleId});
        }

        public IActionResult CreateRole()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
			var result = await _accountService.CreateRoleAsync(model);
			if(result == "OK")
			{
				return RedirectToAction("Role");
			}
			else
			{
				ModelState.AddModelError("", result);
			}
            return View(model);
        }

		public async Task<IActionResult> CreateOwner()
		{
			var list = await _schoolService.GetAllSchoolsAsync();
			ViewBag.Schools = list;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateOwner(OwnerRegisterViewModel model, string selectedSchoolId)
		{
			await _accountService.CreateOwnerAsync(model, selectedSchoolId);
			return View("Index");
		}
    }
}
