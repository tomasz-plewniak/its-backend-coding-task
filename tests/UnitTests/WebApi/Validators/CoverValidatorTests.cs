using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;
using WebAPI.DTOs;
using WebAPI.Validators;

namespace UnitTests.WebApi.Validators;

public class CoverValidatorTests
{
    private static readonly DateOnly _currentDateMock = new(2024, 01, 01);
    
    private CoverValidator _coverValidator;
    
    public CoverValidatorTests()
    {
        IDateTimeService dateTimeService = Substitute.For<IDateTimeService>();
        dateTimeService.DateNow().Returns(_currentDateMock);
        
        _coverValidator = new CoverValidator(dateTimeService);    
    }
    
    [Theory]
    [MemberData(nameof(PastDatesFromDataGenerator))]
    public void ShouldHaveErrorWhenStartDateIsInThePast(DateOnly startDate)
    {
        var model = new Cover() { StartDate = startDate };
        
        var result = _coverValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(cover => cover.StartDate);
    }
    
    [Theory]
    [MemberData(nameof(FutureDatesFromDataGenerator))]
    public void ShouldNotHaveErrorWhenStartDateIsInTheFuture(DateOnly startDate)
    {
        var model = new Cover() { StartDate = startDate };
        
        var result = _coverValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(cover => cover.StartDate);
    }
    
    [Theory]
    [MemberData(nameof(OneYearExceededFromDataGenerator))]
    public void ShouldHaveErrorWhenDateRangeExceedOneYear(DateOnly startDate, DateOnly endDate)
    {
        var model = new Cover() { StartDate = startDate, EndDate = endDate };
        
        var result = _coverValidator.TestValidate(model);
        result.ShouldHaveAnyValidationError();
    }
    
    [Theory]
    [MemberData(nameof(OneYearDontExceededFromDataGenerator))]
    public void ShouldNotHaveErrorWhenDateRangeDontExceedOneYear(DateOnly startDate, DateOnly endDate)
    {
        var model = new Cover() { StartDate = startDate, EndDate = endDate };

        var dateTimeService = new DateTimeService();
        
        var result = _coverValidator.TestValidate(model);
        result.Errors.Should().NotContain(
            x => x.PropertyName == "StartDate and EndDate" 
                 && x.ErrorMessage == "Date range cannot exceed one year");
    }
    
    public static IEnumerable<object[]> PastDatesFromDataGenerator()
    {
        yield return new object[] { _currentDateMock.AddDays(-1) };
        yield return new object[] { _currentDateMock.AddDays(-10) };
        yield return new object[] { _currentDateMock.AddDays(-100) };
        yield return new object[] { _currentDateMock.AddDays(-1000) };
        yield return new object[] { _currentDateMock.AddDays(-10000) };
    }
    
    public static IEnumerable<object[]> FutureDatesFromDataGenerator()
    {
        yield return new object[] { _currentDateMock };
        yield return new object[] { _currentDateMock.AddDays(1) };
        yield return new object[] { _currentDateMock.AddDays(10) };
        yield return new object[] { _currentDateMock.AddDays(100) };
        yield return new object[] { _currentDateMock.AddDays(200) };
    }
    
    public static IEnumerable<object[]> OneYearExceededFromDataGenerator()
    {
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2025, 01, 02)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2025, 03, 02)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2025, 12, 31)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2026, 01, 01)};
        yield return new object[] { new DateOnly(2024, 2, 29), new DateOnly(2025, 03, 1)};
    }
    
    public static IEnumerable<object[]> OneYearDontExceededFromDataGenerator()
    {
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2024, 02, 02)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2024, 08, 02)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2024, 10, 30)};
        yield return new object[] { new DateOnly(2024, 1, 1), new DateOnly(2024, 11, 19)};
        yield return new object[] { new DateOnly(2024, 2, 29), new DateOnly(2025, 2, 28)};
    }
}