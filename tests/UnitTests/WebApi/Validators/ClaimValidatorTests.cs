using ApplicationCore.Functions.Cover.Queries;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WebAPI.DTOs;
using WebAPI.Validators;
using Cover = ApplicationCore.Entities.Cover;

namespace UnitTests.WebApi.Validators;

public class ClaimValidatorTests
{
    private ClaimValidator _claimValidator;
    private IMediator _mediator;
    
    public ClaimValidatorTests()
    {
        _mediator = Substitute.For<IMediator>();
        _claimValidator = new ClaimValidator(_mediator);    
    }
    
    [Theory]
    [InlineData(100000)]
    [InlineData(100001)]
    [InlineData(123001)]
    [InlineData(1000000)]
    [InlineData(10000000)]
    public async Task ShouldHaveError_WhenDamageCostExceed100000(decimal damageCost)
    {
        var model = new Claim()
        {
            DamageCost = damageCost,
            Created = new DateTime(2024, 05, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(claim => claim.DamageCost);
    }
    
    [Theory]
    [InlineData(99999)]
    [InlineData(1)]
    [InlineData(2332)]
    [InlineData(999)]
    [InlineData(23342)]
    public async Task ShouldNotHaveError_WhenDamageCostIsntExceed100000(decimal damageCost)
    {
        var model = new Claim()
        {
            DamageCost = damageCost,
            Created = new DateTime(2024, 05, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(claim => claim.DamageCost);
    }
    
    [Fact]
    public async Task ShouldHaveError_WhenClaimDateIsntBetweenCoverDateRange()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2023, 05, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(claim => claim.Created);
    }
    
    [Fact]
    public async Task ShouldNotHaveError_WhenClaimDateIsBetweenCoverDateRange()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2024, 05, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(claim => claim.Created);
    }
    
    [Fact]
    public async Task ShouldNotHaveError_WhenClaimDateIsEqualToCoverStartDate()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2024, 01, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(claim => claim.Created);
    }
    
    [Fact]
    public async Task ShouldNotHaveError_WhenClaimDateIsEqualToCoverEndDate()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2025, 01, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(claim => claim.Created);
    }
    
    [Fact]
    public async Task ShouldHaveError_WhenCoverIdIsEmptyString()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2025, 01, 01),
            CoverId = String.Empty,
        };
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(claim => claim.CoverId);
    }
    
    [Fact]
    public async Task ShouldHaveError_WhenCoverIdIsntFound()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2025, 01, 01),
            CoverId = Guid.NewGuid().ToString()
        };
        
        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).ReturnsNull();
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(claim => claim.CoverId);
    }
    
    [Fact]
    public async Task ShouldHaveError_WhenCoverStartDateIsDefault()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2025, 01, 01),
            CoverId = Guid.NewGuid().ToString()
        };
        
        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = default,
            EndDate = new DateOnly(2025, 01, 01)
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("StartDate cannot be null or default"));
    }
    
    [Fact]
    public async Task ShouldHaveError_WhenCoverEndDateIsDefault()
    {
        var model = new Claim()
        {
            DamageCost = 10000,
            Created = new DateTime(2025, 01, 01),
            CoverId = Guid.NewGuid().ToString()
        };

        _mediator.Send(Arg.Any<GetCoverByIdQuery>()).Returns(new Cover()
        {
            Id = model.CoverId,
            StartDate = new DateOnly(2024, 01, 01),
            EndDate = default
        });
        
        var result = await _claimValidator.TestValidateAsync(model);
        
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("EndDate cannot be null or default"));
    }
}
