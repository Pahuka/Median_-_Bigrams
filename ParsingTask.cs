using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (lines.Count() > 1)
                try
                {
                    return lines
                        .Skip(1)
                        .Select(x => x.Split(';'))
                        .ToDictionary(x => int.Parse(x[0]),
                                      x => new SlideRecord(int.Parse(x[0]), (SlideType)FindSlideType(x[1]), x[2]));
                }
                catch
                {
                    return new Dictionary<int, SlideRecord>();
                }
            return new Dictionary<int, SlideRecord>();
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
            foreach (var item in lines.Skip(1))
            {
                if (Regex.IsMatch(item, @"(.*?);(.*?);(.*?);(.*?)"))
                {
                    var time = item.Split(';');
                    if (DateTime.TryParseExact((time[2] + " " + time[3]), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime timeFormat))
                    {
                        return lines
                        .Skip(1)
                        .Select(x => x.Split(';'))
                        .Select(x => new VisitRecord(int.Parse(x[0]), int.Parse(x[1]), DateTime.Parse(x[2] + " " + x[3]),
                            slides[int.Parse(x[1])].SlideType));
                    }
                    else throw new FormatException($"Wrong line [{item}]");
                }
                else throw new FormatException($"Wrong line [{item}]");
            }
            return new List<VisitRecord>();
        }
    }
}