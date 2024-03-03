using AutoMapper;

namespace WebAPI.Mappers;

public class ClaimProfile : Profile
{
    public ClaimProfile()
    {
        CreateMap<DTOs.Claim, ApplicationCore.Entities.Claim>();
        CreateMap<ApplicationCore.Entities.Claim, DTOs.Claim>();
    }
}