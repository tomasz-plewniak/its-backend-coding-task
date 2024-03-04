using ApplicationCore.Services;
using FluentAssertions;
using Shared.Enums;

namespace UnitTests.ApplicationCore.Services;

public class CalculateMultiplierServiceTests
{
    [Fact]
    public void CalculateMultiplier_WhenCoverTypeIsYacht_ReturnsCorrectMultiplier()
    {
        const CoverType coverType = CoverType.Yacht;
        const decimal expectedMultiplier = 1.1m;

        decimal result = CalculateMultiplierService.CalculateMultiplier(coverType);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void CalculateMultiplier_WhenCoverTypeIsPassengerShip_ReturnsCorrectMultiplier()
    {
        const CoverType coverType = CoverType.PassengerShip;
        const decimal expectedMultiplier = 1.2m;

        decimal result = CalculateMultiplierService.CalculateMultiplier(coverType);

        result.Should().Be(expectedMultiplier);
    }

    [Fact]
    public void CalculateMultiplier_WhenCoverTypeIsTanker_ReturnsCorrectMultiplier()
    {
        const CoverType coverType = CoverType.Tanker;
        const decimal expectedMultiplier = 1.5m;

        decimal result = CalculateMultiplierService.CalculateMultiplier(coverType);

        result.Should().Be(expectedMultiplier);
    }

    [Theory]
    [InlineData(CoverType.BulkCarrier)]
    [InlineData(CoverType.ContainerShip)]
    public void CalculateMultiplier_WhenCoverTypeIsNotYachtPassengerShipOrTanker_ReturnsCorrectMultiplier(CoverType coverType)
    {
        var expectedMultiplier = 1.3m;

        var result = CalculateMultiplierService.CalculateMultiplier(coverType);

        result.Should().Be(expectedMultiplier);
    }
}