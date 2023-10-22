using KantindenAl.App.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.Services
{
	public interface IStudentService
	{
		Task CreateStudent(StudentViewModel model, string id, string userName);

        Task<List<StudentViewModel>> GetStudentsByParentId(string parentId);
        Task<StudentViewModel> GetStudentByParentId(string parentId, string schoolId);
        Task<StudentViewModel> GetStudentById(string id);

        Task<StudentViewModel> GetStudentBySchoolId(string schoolId, string parentId);
    }
}
