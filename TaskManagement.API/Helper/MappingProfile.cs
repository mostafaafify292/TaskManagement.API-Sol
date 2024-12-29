using AutoMapper;
using TaskManagement.API.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskModel , TaskModelDto>()
                     //.ForMember(T => T.AssignedToUserEmail, D => D.MapFrom(d => d.AssignedTo.Email))
                     //.ForMember(T => T.CategoryName, D => D.MapFrom(d => d.Category.Name))
                     .ReverseMap();
            CreateMap<Category ,  CategoryDto>().ReverseMap();
        }
    }
}
