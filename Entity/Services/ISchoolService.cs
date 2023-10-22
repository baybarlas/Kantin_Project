using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
    public interface ISchoolService
    {
        Task<List<SchoolViewModel>> GetAllSchoolsAsync();

        Task<SchoolViewModel> GetSchoolByIdAsync(string id);

		Task<int> GetSchoolIdByUsernameAsync(string username);

		Task<string> GetSchoolNameByUserIdAsync(int id);
    }
}
