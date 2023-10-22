using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KantindenAl.App.MvcUI.ViewComponents
{
    public class OwnerAndSchoolNavbarViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly ISchoolService _schoolService;

        public OwnerAndSchoolNavbarViewComponent(IAccountService accountService, ISchoolService schoolService)
        {
            _accountService = accountService;
            _schoolService = schoolService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await _accountService.FindUserByUserNameAsync(User.Identity.Name);
            string school = await _schoolService.GetSchoolNameByUserIdAsync(user.Id);
            OwnerAndSchoolNameViewModel model = new OwnerAndSchoolNameViewModel()
            {
                OwnerName = user.FirstName + " " + user.MiddleName + " " + user.LastName,
                SchoolName = school,
                Balance = user.Balance
                
            };
            
            return View(model);
        }
    }
}
