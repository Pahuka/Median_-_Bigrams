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
            throw new NotImplementedException();
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            List<Tuple<T, T>> result = new List<Tuple<T, T>>();

            if (items.Count() < 2) return Enumerable.Empty<Tuple<T, T>>();
            else
            {
                int index = 0;
                for (int i = 0; i < items.Count(); i++)
                {
                    index++;
                    if (index == 2)
                    {
                        index = 0;
                        result.Add(Tuple.Create(items.ElementAt(i - 1), items.ElementAt(i)));
                        i--;
                    }
                }
            }

            return result;
        }
    }
}