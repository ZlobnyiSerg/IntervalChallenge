using IntervalChallenge.Models;

namespace IntervalChallenge.Implementations;

public class Improver
{
    /// <summary>
    /// Осуществляет "группировку" элементов по полуинтервалам, которые определяются точками начала и конца отрезков.
    /// Например, имеем на входе 4 полуинтервала:
    ///
    ///    [---R1---)
    ///          [-----R2-----)
    ///          [---------R3-------)
    ///                  [----R4----)
    ///
    ///  --|-----|--|----|----|-----|---> t (ось времени)
    ///       1    3   2    3    2
    ///
    /// На выходе нужно получить коллекцию из 5 элементов.
    /// Элементом коллекции является список полуинтервалов попадающих в спроецированный отрезок.
    /// Числами в примере отражено количество полуинтервалов в результирующем массиве.
    /// </summary>
    /// <param name="items">Коллекция элементов, которых нужно сгруппировать по интервалам. Важно чтобы она была отсортирована по точкам начала</param>
    /// <returns></returns>
    public static IEnumerable<DateRangeGroupedItems2<T>> Project<T>(params T[] items) where T : IHasDateRange
    {
        if (items.Length <= 0)
            yield break;

        var firstItem = items[0];
        if (items.Length == 1)
            yield return new DateRangeGroupedItems2<T>
            {
                StartDate = firstItem.StartDate,
                EndDate = firstItem.EndDate,
                Items = new Memory<T>(items, 0, 1)
            };

        var current = firstItem;
        var itemStart = 0; // Index of first item to include
        var itemEnd = 0; // Index of last item to include

        for (var i = 1; i < items.Length; i++)
        {
            var item = items[i];
            if (item.StartDate > current.StartDate && item.EndDate < current.EndDate) // Inside current interval
            {
            }
        }
    }
}