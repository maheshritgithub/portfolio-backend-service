using AutoMapper;
using Portfolio.Entities;
using Portfolio.Service.Db.Models;
using System.Diagnostics.CodeAnalysis;
using Portfolio.Entities.RequestModel;
using Portfolio.Entities.ResponseModel;

namespace Portfolio.Service.Misc
{
    /// <summary>
    /// Defines mapping profiles for AutoMapper
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User Mapping Config
            CreateMap<UserRequestModel, User>();
            CreateMap<User, UserResponseModel>();

            // User Details mapping config
            CreateMap<UserDetailsRequestModel, UserDetails>();
            CreateMap<UserDetails, UserDetailsResponseModel>();

            // User experience mapping config
            CreateMap<ExperienceRequestModel, Experience>();
            CreateMap<Experience, ExperienceResponseModel>();

            // User Project mapping config
            CreateMap<ProjectRequestModel, Project>();
            CreateMap<Project, ProjectResponseModel>();
        }
    }
}
