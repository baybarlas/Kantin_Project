using AutoMapper;
using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class WalletActivityService : IWalletActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public WalletActivityService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<WalletActivityViewModel>> GetAllActivities(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var activities = await _unitOfWork.GetRepository<WalletActivity>().GetAll(w => w.UserId == user.Id, w => w.OrderByDescending(w => w.Date));
            return _mapper.Map<List<WalletActivityViewModel>>(activities);
        }

        public async Task AddBalance(UserBillingInformationViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.username);
            var walletActivity = new WalletActivity()
            {
                UserId = user.Id,
                TotalAmount = model.TotalAmount,
                receiptNo = "WLLT" + model.Id,
                Date = DateTime.Now,
                Type = "Yükleme",
                NewBalance = user.Balance + model.TotalAmount
            };
            user.Balance += model.TotalAmount;
            await this.AddActivity(walletActivity);
            var result = await _userManager.UpdateAsync(user);
        }

        public async Task AddActivity(WalletActivity model)
        {
            await _unitOfWork.GetRepository<WalletActivity>().Add(model);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddActivityByUserId(string userId, string type, decimal balance)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var walletActivity = new WalletActivity()
            {
                UserId = user.Id,
                TotalAmount = balance,
                Date = DateTime.Now,
                Type = type,
                NewBalance = user.Balance - balance
            };
            user.Balance -= balance;
            await this.AddActivity(walletActivity);
            var result = await _userManager.UpdateAsync(user);
        }
    }

}
