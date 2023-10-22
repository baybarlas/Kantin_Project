using KantindenAl.App.Entity.Services;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;

        public SaleController(ISaleService saleService, IAccountService accountService, IStudentService studentService)
        {
            _saleService = saleService;
            _accountService = accountService;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetailOwner(string receiptNo)
        {
            var sale = await _saleService.GetSaleByReceiptNo(receiptNo);
            var model = await _saleService.GetSaleDetailsBySaleId(sale.Id);
            var user = await _accountService.FindUserById(sale.UserId.ToString());
            ViewBag.User = user;
            return View(model);
        }

        public async Task<IActionResult> DetailParent(string receiptNo)
        {
            var sale = await _saleService.GetSaleByReceiptNo(receiptNo);
            var model = await _saleService.GetSaleDetailsBySaleId(sale.Id);
            
            return View(model);
        }
    }
}
