using Shared.Enums;

namespace ApplicationCore.Interfaces;

public interface IPremiumService
{
    decimal ComputePremium(DateOnly startDate, DateOnly endDate, CoverType coverType);
}