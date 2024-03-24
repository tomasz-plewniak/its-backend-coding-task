using MediatR;
using Shared.Enums;

namespace ApplicationCore.Functions.Premium.Queries;

public record CalculatePremiumQuery(
    DateOnly StartDate,
    DateOnly EndDate,
    CoverType CoverType)
    : IRequest<decimal>;
