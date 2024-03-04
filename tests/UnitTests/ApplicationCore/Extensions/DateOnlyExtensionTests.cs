using ApplicationCore.Extensions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Extensions;

public class DateOnlyExtensionTests
{
    [Fact]
    public void IsOneYearExceeded_WhenDateRangeIsOneYear_ReturnsFalse()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2023, 1, 1);
        
        bool result = startDate.IsOneYearExceeded(endDate);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsOneYearExceeded_WhenDateRangeIsExactOneYear_ReturnsFalse_LeapYearCase()
    {
        var startDate = new DateOnly(2024, 2, 29);
        var endDate = new DateOnly(2025, 2, 28);
        
        bool result = startDate.IsOneYearExceeded(endDate);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsOneYearExceeded_WhenDateRangeIsOneYearAndOneDay_ReturnsTrue()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2023, 1, 2);
        
        bool result = startDate.IsOneYearExceeded(endDate);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsOneYearExceeded_WhenDateRangeIsOneYearAndOneMonth_ReturnsTrue()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2023, 2, 1);
        
        bool result = startDate.IsOneYearExceeded(endDate);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsDateBetweenInclusive_WhenDateIsBetween_ReturnsTrue()
    {
        var dateToCheck = new DateOnly(2023, 1, 1);
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2024, 1, 1);
        
        bool result = dateToCheck.IsDateBetweenInclusive(startDate, endDate);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsDateBetweenInclusive_WhenDateIsEqualToStartDate_ReturnsTrue()
    {
        var dateToCheck = new DateOnly(2022, 1, 1);
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2024, 1, 1);
        
        bool result = dateToCheck.IsDateBetweenInclusive(startDate, endDate);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsDateBetweenInclusive_WhenDateIsEqualToEndDate_ReturnsTrue()
    {
        var dateToCheck = new DateOnly(2024, 1, 1);
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2024, 1, 1);
        
        bool result = dateToCheck.IsDateBetweenInclusive(startDate, endDate);

        result.Should().BeTrue();
    }
}
