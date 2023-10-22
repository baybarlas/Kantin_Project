using KantindenAl.App.Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IOwnerService _ownerService;
        private readonly ISaleService _saleService;
        private readonly IStudentService _studentService;

        public CartController(ICartService cartService, IProductService productService, IAccountService accountService, IOwnerService ownerService, ISaleService saleService, IStudentService studentService)
        {
            _cartService = cartService;
            _productService = productService;
            _accountService = accountService;
            _ownerService = ownerService;
            _saleService = saleService;
            _studentService = studentService;
        }



        public async Task<IActionResult> Index()
        {
            var schoolId = HttpContext.Session.GetString("SchoolId");
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
            var cartLines = await _cartService.GetCartLines(cart.Id);
            ViewBag.Cart = cart.TotalAmount;
            return View(cartLines);
        }

        public async Task<IActionResult> IncQuantity(int productId)
        {
            await this.AddToCart(productId, 1);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DecQuantity(int productId)
        {
            await this.AddToCart(productId, -1);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
			var schoolId = HttpContext.Session.GetString("SchoolId");
			var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
            await _cartService.AddProductToCartLine(id, quantity, cart);
            return RedirectToAction("List", "Product");
        }

        public async Task<IActionResult> ClearCart(int id)
        {
			var schoolId = HttpContext.Session.GetString("SchoolId");
			var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
            await _cartService.ClearCart(cart);
            return RedirectToAction("List", "Product");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
			var schoolId = HttpContext.Session.GetString("SchoolId");
			var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
            await _cartService.RemoveFromCart(id, cart);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmPayment()
        {
            var schoolId = HttpContext.Session.GetString("SchoolId");
            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            var owner = _ownerService.GetOwnerBySchoolId(schoolId);
            var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
            var cartLines = await _cartService.GetCartLines(cart.Id);
            if(user.Balance < cart.TotalAmount)
            {
                ModelState.AddModelError("", "Bakiye yetersiz. Lütfen Yükleme Yapınız.");
                return RedirectToAction("Wallet", "Parent");
            }
            foreach (var cartLine in cartLines)
            {
                if(cartLine.Product.Stock < cartLine.Quantity)
                {
                    ModelState.AddModelError("", "Stokta ürün bulunmamaktadır.");
                    return RedirectToAction("Index");
                }
            }
            var sale = await _saleService.AddSale(cart, owner, user);
            ViewBag.User = user;
            ViewBag.Student = await _studentService.GetStudentBySchoolId(schoolId, user.Id.ToString());
            return View(sale);
        }
    }
}
