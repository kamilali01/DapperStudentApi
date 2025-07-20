using AutoMapper;
using DapperStudentApi.Dtos;
using DapperStudentApi.Models;

namespace DapperStudentApi.Mapping
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
