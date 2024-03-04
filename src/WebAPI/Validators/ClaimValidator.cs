using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Validators;

public class ClaimValidator : AbstractValidator<Claim>
{
    private readonly ICoverRepository _coverRepository;
    private const decimal MaxDamageCost = 100000;

    public ClaimValidator(ICoverRepository coverRepository)
    {
        _coverRepository = coverRepository;
        
        RuleFor(x => x.DamageCost)
            .LessThan(MaxDamageCost)
            .WithMessage($"DamageCost cannot exceed {MaxDamageCost}");

        RuleFor(x => x)
            .MustAsync( async (x, cancellationToken) =>
            {
                var cover = await _coverRepository.GetItemAsync(x.Id);

                var dwa = DateOnly.FromDateTime(x.Created);

                var result = dwa.IsDateBetweenInclusive(cover.StartDate, cover.EndDate);

                return result;
            })
            .OverridePropertyName("Created")
            .WithMessage("Claim date must be between cover date range");
    }
}
