using AutoMapper;

namespace WebAPI.Mappers;

public class CoverProfile : Profile
{
    public CoverProfile()
    {
        CreateMap<DTOs.Cover, ApplicationCore.Entities.Cover>();
        CreateMap<ApplicationCore.Entities.Cover, DTOs.Cover>();
    }
}