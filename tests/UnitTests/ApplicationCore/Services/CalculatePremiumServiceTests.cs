using ApplicationCore.Services;
using FluentAssertions;
using Shared.Enums;

namespace UnitTests.ApplicationCore.Services;

public class CalculatePremiumServiceTests
{
    CalculatePremiumService _calculatePremiumService;
    
    public CalculatePremiumServiceTests()
    {
        _calculatePremiumService = new CalculatePremiumService();
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsYacht_1_Day_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 2);
        var coverType = CoverType.Yacht;
        var expectedPremium = 1375m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsPassengerShip_1_Day_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 2);
        var coverType = CoverType.PassengerShip;
        var expectedPremium = 1500m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsTanker_1_Day_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 2);
        var coverType = CoverType.Tanker;
        var expectedPremium = 1875m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsBulkCarrier_1_Day_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 2);
        var coverType = CoverType.BulkCarrier;
        var expectedPremium = 1625m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsYacht_31_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 2, 1);
        var coverType = CoverType.Yacht;
        var expectedPremium = 42556.250m; 

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsPassengerShip_31_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 2, 1);
        var coverType = CoverType.PassengerShip;
        var expectedPremium = 46470.000m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsTanker_31_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 2, 1);
        var coverType = CoverType.Tanker;
        var expectedPremium = 58087.5m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsBulkCarrier_31_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 2, 1);
        var coverType = CoverType.BulkCarrier;
        var expectedPremium = 50342.5m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsYacht_151_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 05, 31);
        var coverType = CoverType.Yacht;
        var expectedPremium = 199333.75m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsPassengerShip_151_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 05, 31);
        var coverType = CoverType.PassengerShip;
        var expectedPremium = 222885m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsTanker_151_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 05, 31);
        var coverType = CoverType.Tanker;
        var expectedPremium = 278606.25m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
    
    [Fact]
    public void ComputePremium_WhenCoverTypeIsBulkCarrier_151_Days_ReturnsCorrectPremium()
    {
        var startDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 05, 31);
        var coverType = CoverType.BulkCarrier;
        var expectedPremium = 241458.75m;

        var result = _calculatePremiumService.ComputePremium(startDate, endDate, coverType);

        result.Should().Be(expectedPremium);
    }
}