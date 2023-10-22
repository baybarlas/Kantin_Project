using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IAccountService _accountService;

        public OwnerService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<string> AddProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public OwnerViewModel GetOwnerBySchoolId(string id)
        {
            var user = _accountService.FindUserBySchoolId(id);
            return user;
        }
    }
}
