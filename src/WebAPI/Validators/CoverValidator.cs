using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Validators;

public class CoverValidator : AbstractValidator<Cover>
{
    private readonly IDateTimeService _dateTimeService;

    public CoverValidator(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
        
        RuleFor(x => x.StartDate).GreaterThanOrEqualTo(dateTimeService.DateNow())
            .WithMessage("StartDate cannot be in the past");
        
        RuleFor(x => new {x.StartDate, x.EndDate})
            .Must(x => x.StartDate.IsOneYearExceeded(x.EndDate) == false)
            .OverridePropertyName("StartDate and EndDate") 
            .WithMessage("Date range cannot exceed one year");
    }
}
