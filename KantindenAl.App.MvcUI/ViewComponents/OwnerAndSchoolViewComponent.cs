using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.ViewComponents
{
    public class OwnerAndSchoolViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly ISchoolService _schoolService;

        public OwnerAndSchoolViewComponent(IAccountService service, ISchoolService schoolService)
        {
            _accountService = service;
            _schoolService = schoolService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            string school = await _schoolService.GetSchoolNameByUserIdAsync(user.Id);
            OwnerAndSchoolNameViewModel model = new OwnerAndSchoolNameViewModel()
            {
                OwnerName = user.StoreName,
                SchoolName = school
            };
            return View(model);
        }
    }
}
