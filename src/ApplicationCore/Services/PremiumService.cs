using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Shared.Enums;

namespace ApplicationCore.Services;

public class PremiumService : IPremiumService
{
    private const decimal BaseDayRate = 1250;
    
    public decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType)
    {
        decimal multiplier = CalculateMultiplierService.CalculateMultiplier(coverType);

        var premiumPerDay = BaseDayRate * multiplier;
        var insuranceLength = startDate.ReturnNumberOfDaysBetweenDates(endDate);
        
        var totalPremium = 0m;

        for (var i = 0; i < insuranceLength; i++)
        {
            switch (i)
            {
                case < 30:
                    totalPremium += premiumPerDay;
                    break;
                case < 150 when coverType == CoverType.Yacht:
                    totalPremium += premiumPerDay - premiumPerDay * 0.05m;
                    break;
                case < 150:
                    totalPremium += premiumPerDay - premiumPerDay * 0.02m;
                    break;
                case var _ when i < insuranceLength && coverType == CoverType.Yacht:
                    totalPremium += premiumPerDay - premiumPerDay * 0.03m;
                    break;
                case var _ when i < insuranceLength:
                    totalPremium += premiumPerDay - premiumPerDay * 0.01m;
                    break;
            }
        }

        return totalPremium;
    }
}