namespace ApplicationCore.Interfaces;

public interface IDateTimeService
{
    DateTime DateTimeNow();
    
    DateOnly DateNow();
}