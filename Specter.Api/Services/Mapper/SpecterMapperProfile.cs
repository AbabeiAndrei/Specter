using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Data.Entities;

namespace Specter.Api.Mapper
{
    public class SpecterMappingProfile : Profile 
    {
        public SpecterMappingProfile() 
        {
            CreateMap<UserModel, ApplicationUser>().ReverseMap();
            CreateMap<LoginUserModel, ApplicationUser>().ReverseMap();
            CreateMap<TimesheetModel, Timesheet>().ReverseMap();
            CreateMap<TimesheetUpdateModel, Timesheet>().ReverseMap();
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<DeliveryModel, Delivery>().ReverseMap();
            CreateMap<DeliveryExModel, Delivery>().ReverseMap();
            CreateMap<ProjectModel, Project>().ReverseMap();
            CreateMap<ProjectExModel, Project>().ReverseMap();
        }
    }
}