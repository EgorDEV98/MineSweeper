namespace MineSweeper.Application.Interfaces;

public interface IDateTimeProvider
{
    public DateTimeOffset GetCurrentDateTime();
}