using IntervalChallenge.Models;

namespace IntervalChallenge.Implementations;

public class Legacy
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
    public static IEnumerable<DateRangeGroupedItems<T>> Project<T>(IList<T> items) where T : IHasDateRange
    {
        if (items.Count <= 1)
        {
            if (items.Count == 0)
            {
                yield break;
            }

            yield return new DateRangeGroupedItems<T>()
            {
                StartDate = items[0].StartDate,
                EndDate = items[0].EndDate,
                Items = items
            };
        }
        else
        {
            var endOrdered = items.OrderBy(i => i.EndDate).ToList();
            var active = new List<T>();
            DateTime? last = null;
            foreach (var pair in TwoArrayIterator(items, endOrdered))
            {
                var current = pair.Key == 1 ? pair.Value.StartDate : pair.Value.EndDate;
                if (last != null && current != last)
                {
                    yield return new DateRangeGroupedItems<T>
                    {
                        StartDate = last.Value,
                        EndDate = current,
                        Items = active.ToList()
                    };
                }

                if (pair.Key == 1)
                {
                    active.Add(pair.Value);
                }
                else
                {
                    active.Remove(pair.Value);
                }

                last = current;
            }
        }
    }

    private static IEnumerable<KeyValuePair<int, T>> TwoArrayIterator<T>(IList<T> arr1, IList<T> arr2)
        where T : IHasDateRange
    {
        var index1 = 0;
        var index2 = 0;
        while (index1 < arr1.Count || index2 < arr2.Count)
        {
            if (index1 >= arr1.Count)
                yield return new KeyValuePair<int, T>(2, arr2[index2++]);
            else if (index2 >= arr2.Count)
                yield return new KeyValuePair<int, T>(1, arr1[index1++]);
            else
            {
                var elt1 = arr1[index1];
                var elt2 = arr2[index2];
                if (elt1.StartDate < elt2.EndDate)
                {
                    index1++;
                    yield return new KeyValuePair<int, T>(1, elt1);
                }
                else
                {
                    index2++;
                    yield return new KeyValuePair<int, T>(2, elt2);
                }
            }
        }
    }
}