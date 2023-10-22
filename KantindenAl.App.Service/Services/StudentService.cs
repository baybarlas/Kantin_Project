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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountservice;
        private readonly IMapper _mapper;
        public StudentService(IUnitOfWork unitOfWork, IAccountService accountservice, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _accountservice = accountservice;
            _mapper = mapper;
        }

        public async Task CreateStudent(StudentViewModel model, string id, string userName)
        {
            var user = await _accountservice.FindUserByUserNameAsync(userName);
            var student = new Student()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ClassName = model.ClassName,
                SchoolId = Convert.ToInt32(id), 
                StdNo = model.StdNo,
                UserId = user.Id    
            };
            await _unitOfWork.GetRepository<Student>().Add(student);
            await _unitOfWork.CommitAsync();

        }

        public async Task<StudentViewModel> GetStudentById(string id)
        {
            var student = await _unitOfWork.GetRepository<Student>().Get(s => s.Id == Convert.ToInt32(id));
            return _mapper.Map<StudentViewModel>(student);
        }

        public async Task<StudentViewModel> GetStudentByParentId(string parentId, string schoolId)
        {
            var student = await _unitOfWork.GetRepository<Student>().Get(s => s.UserId == Convert.ToInt32(parentId) && s.SchoolId == Convert.ToInt32(schoolId));
            return _mapper.Map<StudentViewModel>(student);
        }

        public async Task<StudentViewModel> GetStudentBySchoolId(string id, string parentId)
        {
            var student = await _unitOfWork.GetRepository<Student>().Get(s => s.SchoolId == Convert.ToInt32(id) && s.UserId == Convert.ToInt32(parentId));
            return _mapper.Map<StudentViewModel>(student);
        }

        public async Task<List<StudentViewModel>> GetStudentsByParentId(string parentId)
        {
            var students = await _unitOfWork.GetRepository<Student>().GetAll(s => s.UserId ==Convert.ToInt32(parentId));
            return _mapper.Map<List<StudentViewModel>>(students);
        }
    }
}
