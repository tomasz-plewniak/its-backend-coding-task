using NodaTime;

namespace ApplicationCore.Extensions;

public static class DateOnlyExtension
{
    public static bool IsOneYearExceeded(this DateOnly startDate, DateOnly endDate)
    {
        var localDate1 = new LocalDate(startDate.Year, startDate.Month, startDate.Day);
        var localDate2 = new LocalDate(endDate.Year, endDate.Month, endDate.Day);

        var period = Period.Between(localDate1, localDate2, PeriodUnits.Years | PeriodUnits.Months | PeriodUnits.Days);
        
        switch (period.Years)
        {
            case > 1:
            case 1 when period.Months > 0: 
            case 1 when period.Days > 0:
                return true;
            default:
                return false;
        }
    }
    
    public static bool IsDateBetweenInclusive(this DateOnly dateToCheck, DateOnly startDate, DateOnly endDate)
    {
        return dateToCheck >= startDate && dateToCheck <= endDate;
    }
    
    public static int ReturnNumberOfDaysBetweenDates(this DateOnly startDate, DateOnly endDate)
    {
        var localDate1 = new LocalDate(startDate.Year, startDate.Month, startDate.Day);
        var localDate2 = new LocalDate(endDate.Year, endDate.Month, endDate.Day);

        var period = Period.DaysBetween(localDate1, localDate2);
        return period;
    }
}