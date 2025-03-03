using MineSweeper.Application.Interfaces;

namespace MineSweeper.Application.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetCurrentDateTime()
        => DateTimeOffset.Now;
}