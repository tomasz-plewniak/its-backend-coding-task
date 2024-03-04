using Shared.Enums;

namespace ApplicationCore.Interfaces;

public interface ICalculatePremiumService
{
    decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType);
}