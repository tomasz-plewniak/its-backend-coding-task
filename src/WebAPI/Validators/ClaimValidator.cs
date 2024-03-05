using ApplicationCore.Extensions;
using ApplicationCore.Functions.Cover.Queries;
using ApplicationCore.Interfaces;
using FluentValidation;
using MediatR;
using WebAPI.DTOs;

namespace WebAPI.Validators;

public class ClaimValidator : AbstractValidator<Claim>
{
    private readonly IMediator _mediator;
    private const decimal MaxDamageCost = 100000;

    public ClaimValidator(IMediator mediator)
    {
        _mediator = mediator;
        
        RuleFor(x => x.DamageCost)
            .LessThan(MaxDamageCost)
            .WithMessage($"DamageCost cannot exceed {MaxDamageCost}");

        #region Determinate if created date is in betten the cover start and end date.

        RuleFor(x => x.Created)
            .NotNull()
            .WithMessage("Created cannot be null")
            .DependentRules(() =>
            {
                RuleFor(x => x.CoverId)
                    .NotNull()
                    .WithMessage("CoverId cannot be null");
            })
            .DependentRules(() =>
            {
                RuleFor(x => new { x.CoverId, x.Created })
                    .CustomAsync(async (x, context, cancellationToken) =>
                    {
                        var cover = await _mediator.Send(new GetCoverByIdQuery(x.CoverId));

                        if (cover == null || cover == default)
                        {
                            context.AddFailure(nameof(Claim.CoverId),"Not found any cover with this CoverId");
                            return;
                        }

                        if (cover.StartDate == null || cover.StartDate == default)
                        {
                            context.AddFailure("StartDate cannot be null or default");
                            return;
                        }

                        if (cover.EndDate == null || cover.EndDate == default)
                        {
                            context.AddFailure("EndDate cannot be null or default");
                            return;
                        }

                        var dateOnly = DateOnly.FromDateTime(x.Created);
                        bool isDateBetween = dateOnly.IsDateBetweenInclusive(cover.StartDate, cover.EndDate);

                        if (isDateBetween == false)
                        {
                            context.AddFailure(nameof(Claim.Created), "Created date is not between the cover start and end date");
                        }
                    });
            });
        
        #endregion
        
    }
}
