using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace linq_slideviews
{
    public class ParsingTask
    {
        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
        /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
        /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            var result = new Dictionary<int, SlideRecord>();

            if (lines.Count() > 1)
                foreach (var item in lines.Skip(1))
                {
                    var itemSplit = item.Split(';');

                    if (Regex.IsMatch(item, @"(^\d+);(theory|exercise|quiz);(\w+)"))
                    {
                        result.Add(int.Parse(itemSplit[0]), new SlideRecord(int.Parse(itemSplit[0]),
                            (SlideType)FindSlideType(itemSplit[1]), itemSplit[2]));
                    }
                }
            return result;
        }

        private static int FindSlideType(string type)
        {
            List<string> list = Enum.GetNames(typeof(SlideType)).Select(x => x.ToLower()).ToList();
            return list.IndexOf(type);
        }

        /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
        /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
        /// Такой словарь можно получить методом ParseSlideRecords</param>
        /// <returns>Список информации о посещениях</returns>
        /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            var result = new List<VisitRecord>();

            foreach (var item in lines.Skip(1))
            {
                if (Regex.IsMatch(item, @"(^\d+);(\d+);(.*?);(.+)"))
                {
                    var itemSplit = item.Split(';');
                    if (DateTime.TryParse((itemSplit[2] + " " + itemSplit[3]), out DateTime timeFormat)
                       & slides.ContainsKey(int.Parse(itemSplit[1])))
                    {
                        result.Add(new VisitRecord(int.Parse(itemSplit[0]), int.Parse(itemSplit[1]),
                            DateTime.Parse(itemSplit[2] + " " + itemSplit[3]),
                            slides[int.Parse(itemSplit[1])].SlideType));
                    }
                    else throw new FormatException($"Wrong line [{item}]");
                }
                else throw new FormatException($"Wrong line [{item}]");
            }
            return result;
        }
    }
}