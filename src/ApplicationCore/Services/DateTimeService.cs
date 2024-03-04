using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime DateTimeNow()
    {
        return DateTime.Now;
    }

    public DateOnly DateNow()
    {
        return DateOnly.FromDateTime(DateTime.Now);
    }
}