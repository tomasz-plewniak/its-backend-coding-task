using Shared.Enums;

namespace ApplicationCore.Services;

public static class CalculateMultiplierService
{
    public static decimal CalculateMultiplier(CoverType coverType)
    {
        return coverType switch
        {
            CoverType.Yacht => 1.1m,
            CoverType.PassengerShip => 1.2m,
            CoverType.Tanker => 1.5m,
            _ => 1.3m
        };
    }
}