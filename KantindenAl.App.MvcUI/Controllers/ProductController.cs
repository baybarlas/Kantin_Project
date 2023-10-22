using KantindenAl.App.Entity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace KantindenAl.App.MvcUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;


        public ProductController(IProductService productService, IAccountService accountService, ICategoryService categoryService)
        {
            _productService = productService;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(string? id, string? search)
        {
            var schoolId = HttpContext.Session.GetString("SchoolId");
            var model = await _productService.GetAllProductsBySchoolId(Convert.ToInt32(schoolId));
            if (search != null)
            {
                model = await _productService.GetSearchedProductsBySchoolId(search, Convert.ToInt32(schoolId));
                return View(model);
            }

            if (id != null)
            {
                if(id == "0")
                {
                    return View(model);
                }
                model = await _productService.GetProductsByCategoryAndSchoolId(id, Convert.ToInt32(schoolId));
                return View(model);

            }
            return View(model);
        }

        public async Task<IActionResult> Detail(string? id)
        {
            var model = await _productService.GetProductById(id);
            ViewBag.Category = await _categoryService.GetCategoryByProduct(model);
            return View(model);
        }
    }
}
