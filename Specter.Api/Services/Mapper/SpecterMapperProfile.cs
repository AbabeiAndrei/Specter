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
            CreateMap<TimesheetModel, Timesheet>().ReverseMap()
                                                  .ForMember(ts => ts.InternalId, mc => mc.MapFrom(ts => ts.InternalIdMap))
                                                  .ForMember(ts => ts.Category, mc => mc.MapFrom(ts => ts.Category.Name))
                                                  .ForMember(ts => ts.Delivery, mc => mc.MapFrom(ts => ts.Delivery.Name))
                                                  .ForMember(ts => ts.Project, mc => mc.MapFrom(ts => ts.Project.Name));
            CreateMap<TimesheetUpdateModel, Timesheet>().ReverseMap();
            CreateMap<CategoryModel, Category>().ReverseMap();
            CreateMap<DeliveryModel, Delivery>().ReverseMap();
            CreateMap<DeliveryExModel, Delivery>().ReverseMap()
                                                  .ForMember(d => d.Project, mc => mc.MapFrom(d => d.Project.Name));
            CreateMap<ProjectModel, Project>().ReverseMap();
            CreateMap<ProjectExModel, Project>().ReverseMap();
        }
    }
}