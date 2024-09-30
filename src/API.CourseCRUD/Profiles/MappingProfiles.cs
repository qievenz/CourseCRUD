using AutoMapper;
using Core.CourseCRUD.DTOs;
using Core.CourseCRUD.Entities;

namespace API.CourseCRUD.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
        }
    }
}
