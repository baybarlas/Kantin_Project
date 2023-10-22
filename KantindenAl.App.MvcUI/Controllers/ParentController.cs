using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.Controllers
{

	public class ParentController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IStudentService _studentService;
		private readonly ISchoolService _schoolService;
		private readonly IWalletActivityService _walletActivityService;
		private readonly IProductService _productService;
		private readonly ISaleService _saleService;


        public ParentController(IAccountService accountService, IStudentService studentService, ISchoolService schoolService, IWalletActivityService walletActivityService, IProductService productService, ISaleService saleService)
        {
            _accountService = accountService;
            _studentService = studentService;
            _schoolService = schoolService;
            _walletActivityService = walletActivityService;
            _productService = productService;
            _saleService = saleService;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			var result = await _accountService.CreateUserAsync(model);
			if(result == "OK")
			{
				return RedirectToAction("Login");
			}
			else
			{
				ModelState.AddModelError("", result);
			}
			return View(model);
		}

		public IActionResult Login(string? returnUrl)
		{
			LoginViewModel model = new LoginViewModel()
			{
				ReturnUrl = returnUrl
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var result = await _accountService.FindUserByNameAsync(model);
			if(result == "OK")
			{
				if (User.IsInRole("Admin"))
				{
					return RedirectToAction("Index", "Admin");
				}
				else if (User.IsInRole("Owner"))
				{
					return RedirectToAction("Index", "Owner");
				}
				else
				{
                    return RedirectToAction("SelectStudent");
                }				
			}
			else if(result == "Kullanıcı bulunamadı.")
			{
				ModelState.AddModelError("", result);
				return View(model);
			}
			else
			{
				ModelState.AddModelError("", "Kullanıcı adı verya şifre hatalı");
			}
			return View(model);
		}

		public async Task<IActionResult> SelectStudent()
		{
			var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
			var model = await _studentService.GetStudentsByParentId(user.Id.ToString());
			return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> SelectStudent(string selectedStudentId)
        {

			var student = await _studentService.GetStudentById(selectedStudentId);
			HttpContext.Session.SetString("SchoolId", student.SchoolId.ToString());
			return RedirectToAction("List","Product");
        }

        public async Task<IActionResult> Logout()
		{
			await _accountService.LogoutAsync();
			return RedirectToAction("Index","Home");
		}

		public async Task<IActionResult> Account()
		{
			var model = await _accountService.FindUserByUserNameAsync(User.Identity.Name);			
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Account(UserViewModel model)
		{
			string result = await _accountService.EditUserAsync(model);
            if (result == "OK")
            {
                return RedirectToAction("Account");
            }
            else
            {
                ModelState.AddModelError("", result);
            }
            return View(model);
        }

		public async Task<IActionResult> Order()
		{
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
			ViewBag.User = user;
			var sales = await _saleService.GetSalesByUserId(user.Id);
            return View(sales);
        }

        public async Task<IActionResult> Student()
        {
			var parent = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
			ViewBag.Students = await _studentService.GetStudentsByParentId(parent.Id.ToString());
			return View();
        }
		[HttpPost]
        public async Task<IActionResult> Student(string selectedStudentId)
        {
            var student = await _studentService.GetStudentById(selectedStudentId);
			HttpContext.Session.SetString("SchoolId", student.SchoolId.ToString());
            if (selectedStudentId == null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("List", "Product");
            }
        }

        public async Task<IActionResult> RegisterStudent()
		{
			ViewBag.Schools = await _schoolService.GetAllSchoolsAsync();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> RegisterStudent(StudentViewModel model, string selectedSchoolId)
		{
			await _studentService.CreateStudent(model, selectedSchoolId, User.Identity.Name);
			return RedirectToAction("Student");
		}

		public IActionResult StudentDetail(string id)
		{
			var model = _studentService.GetStudentById(id);
			return View(model);
		}

        public async Task<IActionResult> Wallet()
        {
            var activities = await _walletActivityService.GetAllActivities(User.Identity.Name);
			ViewBag.Activities = activities;
            var model = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            return View(model);
        }

		public IActionResult TopUp(string id, decimal balance)
		{
			ViewBag.Balance = balance;
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> TopUp(UserBillingInformationViewModel model)
        {
			await _walletActivityService.AddBalance(model);
            return RedirectToAction("Wallet");
        }

		

    }
}


