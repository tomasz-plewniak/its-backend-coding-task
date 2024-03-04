using ApplicationCore.Interfaces;
using FluentValidation;
using NodaTime;
using WebAPI.DTOs;

namespace WebAPI.Validators;

public class CoverValidator : AbstractValidator<Cover>
{
    private readonly IDateTimeService _dateTimeService;

    public CoverValidator(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;

        var test = _dateTimeService.DateNow();

        RuleFor(x => x.StartDate).GreaterThanOrEqualTo(dateTimeService.DateNow())
            .WithMessage("StartDate cannot be in the past");
        
        RuleFor(x => new {x.StartDate, x.EndDate})
            .Must(x => OneYearIsExceeded(x.StartDate, x.EndDate) == false)
            .OverridePropertyName("StartDate and EndDate") 
            .WithMessage("Date range cannot exceed one year");
    }
    
    private bool OneYearIsExceeded(DateOnly startDate, DateOnly endDate)
    {
        var localDate1 = new LocalDate(startDate.Year, startDate.Month, startDate.Day);
        var localDate2 = new LocalDate(endDate.Year, endDate.Month, endDate.Day);

        var period = Period.Between(localDate1, localDate2, PeriodUnits.Years | PeriodUnits.Months | PeriodUnits.Days);

        return period switch
        {
            { Years: > 1 , Days: 0} => true,
            { Years: 1, Days: > 0 } => true,
            _ => false
        };
    }
}