using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
        /// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var medianList = items.ToList();
            int index = medianList.Capacity;
            if (index == 0) throw new InvalidOperationException();
            if (index % 2 == 0) return medianList
                          .OrderBy(x => x)
                          .Skip((index / 2) - 1)
                          .Take(2)
                          .Sum()/2;

            return medianList
                        .OrderBy(x => x)
                        .Skip(index/2)
                        .First();
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            List<Tuple<T, T>> result = new List<Tuple<T, T>>();
            var values = items.GetEnumerator();
            if (!values.MoveNext()) yield break;
            else
            {
                T tempValue = values.Current;
                int index = 1;
                while (values.MoveNext())
                {
                    index++;
                    if (index == 2)
                    {
                        yield return Tuple.Create(tempValue, values.Current);
                        index = 1;
                        tempValue = values.Current;
                    }
                    tempValue = values.Current;
                }

                //int index = 0;
                //for (int i = 0; i < items.Count(); i++)
                //{
                //    index++;
                //    if (index == 2)
                //    {
                //        index = 0;
                //        result.Add(Tuple.Create(items.ElementAt(i - 1), items.ElementAt(i)));
                //        i--;
                //    }
                //}
            }

            //return result;
        }
    }
}