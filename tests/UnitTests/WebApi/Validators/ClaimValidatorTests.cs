using FluentValidation.TestHelper;
using WebAPI.DTOs;
using WebAPI.Validators;

namespace UnitTests.WebApi.Validators;

public class ClaimValidatorTests
{
    private ClaimValidator _claimValidator;
    
    public ClaimValidatorTests()
    {
        _claimValidator = new ClaimValidator();    
    }
    
    [Theory]
    [InlineData(100000)]
    [InlineData(100001)]
    [InlineData(123001)]
    [InlineData(1000000)]
    [InlineData(10000000)]
    public void ShouldHaveError_WhenDamageCostExceed100000(decimal damageCost)
    {
        var model = new Claim() { DamageCost = damageCost };
        
        var result = _claimValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(claim => claim.DamageCost);
    }
    
    [Theory]
    [InlineData(99999)]
    [InlineData(1)]
    [InlineData(2332)]
    [InlineData(999)]
    [InlineData(23342)]
    public void ShouldNotHaveError_WhenDamageCostIsntExceed100000(decimal damageCost)
    {
        var model = new Claim() { DamageCost = damageCost };
        
        var result = _claimValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(claim => claim.DamageCost);
    }
}