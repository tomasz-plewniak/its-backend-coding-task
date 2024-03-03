using AutoMapper;
using WebAPI.Mappers;

namespace UnitTests.WebApi.Mappers;

public class MappingProfileTests
{
    [Fact]
    public void ValidateClaimProfileMappingConfigurationTest()
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new ClaimProfile());
            });

        IMapper mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
    
    [Fact]
    public void ValidateCoverProfileMappingConfigurationTest()
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CoverProfile());
            });

        IMapper mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}