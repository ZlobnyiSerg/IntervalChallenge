namespace IntervalChallenge.Models;

public interface IHasDateRange
{
    DateTime StartDate { get; }
    DateTime EndDate { get; }
}

public record Interval(DateTime StartDate, DateTime EndDate) : IHasDateRange;