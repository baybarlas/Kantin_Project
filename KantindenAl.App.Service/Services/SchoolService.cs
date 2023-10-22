using AutoMapper;
using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.Entity.Entities;
using KantindenAl.App.Entity.Services;
using KantindenAl.App.Entity.UnitOfWork;
using KantindenAl.App.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Service.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

		public SchoolService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_accountService = accountService;
		}

		public async Task<List<SchoolViewModel>> GetAllSchoolsAsync()
        {
            var list = await _unitOfWork.GetRepository<School>().GetAllAsync();
            return _mapper.Map<List<SchoolViewModel>>(list);
        }

        public async Task<SchoolViewModel> GetSchoolByIdAsync(string id)
        {
            var school = await _unitOfWork.GetRepository<School>().GetById(Convert.ToInt32(id));
            return _mapper.Map<SchoolViewModel>(school);
        }

		public async Task<int> GetSchoolIdByUsernameAsync(string username)
		{
		    var user = await _accountService.FindUserByUserNameAsync(username);
            return user.SchoolId;
		}

		public async Task<string> GetSchoolNameByUserIdAsync(int id)
        {
            var user = await _accountService.FindUserById(id.ToString());
            var school = await _unitOfWork.GetRepository<School>().Get(x => x.Id == user.SchoolId);
            return school.Name;
        }
    }
}
