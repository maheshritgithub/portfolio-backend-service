using AutoMapper;
using Portfolio.Entities;
using Portfolio.Service.Db;
using System.Diagnostics.CodeAnalysis;


namespace Portfolio.Service.Misc;

/// <summary>
/// Class defines the mapping profiles for auto mapper
/// </summary>
[ExcludeFromCodeCoverage]
internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}
