using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Validators;

public class ClaimValidator : AbstractValidator<Claim>
{
    private const decimal MaxDamageCost = 100000;
    
    public ClaimValidator()
    {
        RuleFor(x => x.DamageCost).LessThan(MaxDamageCost)
            .WithMessage($"DamageCost cannot exceed {MaxDamageCost}");
    }
}
