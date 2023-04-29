namespace IntervalChallenge.Models;

public class DateRangeGroupedItems<T>
    where T : IHasDateRange
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<T> Items { get; set; }
}

public record DateRangeGroupedItems2<T>
    where T : IHasDateRange
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ReadOnlyMemory<T> Items { get; internal set; }
}