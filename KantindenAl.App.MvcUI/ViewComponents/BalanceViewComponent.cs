using KantindenAl.App.Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.ViewComponents
{
    public class BalanceViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        public BalanceViewComponent(IAccountService accountService, ICartService cartService)
        {
            _accountService = accountService;
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            if (user != null)
            {
				var schoolId = HttpContext.Session.GetString("SchoolId");
				var cart = await _cartService.GetCart(user.Id, Convert.ToInt32(schoolId));
                var cartLines = await _cartService.GetCartLines(cart.Id);
                ViewBag.CartLines = cartLines.Count;
                return View(user);
            }
            return View();

        }


    }
}
