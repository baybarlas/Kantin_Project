using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface IWalletActivityService
    {
        Task AddActivity(WalletActivity model);

        Task AddActivityByUserId(string userId, string type, decimal balance);

        Task AddBalance(UserBillingInformationViewModel model);

        Task<List<WalletActivityViewModel>> GetAllActivities(string username);
    }
}
