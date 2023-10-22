using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using KantindenAl.App.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KantindenAl.App.MvcUI.Controllers
{
    public class OwnerController : Controller
    {
		private readonly IAccountService _accountService;
        private readonly IOwnerService _ownerService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ISchoolService _schoolService;
        private readonly ISaleService _saleService;
        private readonly IWalletActivityService _walletActivitySevice;

        public OwnerController(IAccountService accountService, IOwnerService ownerService, ICategoryService categoryService, IProductService productService, ISchoolService schoolService, ISaleService saleService, IWalletActivityService walletActivitySevice)
        {
            _accountService = accountService;
            _ownerService = ownerService;
            _categoryService = categoryService;
            _productService = productService;
            _schoolService = schoolService;
            _saleService = saleService;
            _walletActivitySevice = walletActivitySevice;
        }

        public async Task<IActionResult> Index()
		{
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var sales = await _saleService.GetSalesByOwnerId(user.Id);
            var todaySales = sales.Where(s => s.CreatedDate < s.CreatedDate.AddDays(1));
            ViewBag.TodaySales = todaySales.Count();
            decimal todayTotalSale = 0;
            decimal totalSale = 0;
            foreach (var sale in todaySales)
            {
                todayTotalSale += (decimal)sale.TotalAmount;
            }
            ViewBag.TodayTotalSales = todayTotalSale;
            foreach (var sale in sales)
            {
                totalSale += (decimal)sale.TotalAmount;
            }
            ViewBag.TotalSale = totalSale;
            return View(sales);

		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            var result = await _accountService.FindUserByNameAsync(model);
            if (result == "OK")
            {
                if (User.IsInRole("Parent"))
                {
                    return RedirectToAction("SelectStudent", "Parent");
                }
                else
                {
					return RedirectToAction("Index", "Owner");
				}
            }
            else if (result == "Kullanıcı bulunamadı.")
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

        public async Task<IActionResult> Wallet()
        {
            var activities = await _walletActivitySevice.GetAllActivities(User.Identity.Name);
            ViewBag.Activities = activities;
            var model = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Withdraw(decimal Balance)
        {
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            await _accountService.UpdateUser(user);
            await _walletActivitySevice.AddActivityByUserId(user.Id.ToString(), "Para Çekme", Balance);
            return RedirectToAction("Wallet");
        }

        public async Task<IActionResult> AddProduct()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name"); //asp-items tag helper ile kullanılır.
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductViewModel model, IFormFile formFile)
        {
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            model.OwnerId = user.Id;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
            var stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);
            model.ImageUrl = "/images/" + formFile.FileName;
            model.SchoolId = user.SchoolId;
            await _productService.AddProduct(model);
            return RedirectToAction("Products");
        }

        public IActionResult ProductList()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
			var schoolId = await _schoolService.GetSchoolIdByUsernameAsync(User.Identity.Name);
			var model = await _productService.GetAllProductsBySchoolId(schoolId);
            return View(model);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productService.GetProductById(id.ToString());
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel model)
        {
            await _productService.UpdateProduct(model);
            return RedirectToAction("Products", "Owner");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
