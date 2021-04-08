using System;
using System.Collections.Generic;
using System.Linq;

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
                return lines
                .Skip(1)
                .Select(x => x.Split(';'))
                .ToDictionary(x => int.Parse(x[0]), x => new SlideRecord(int.Parse(x[0]), (SlideType)FindSlideType(x[1]), x[2]));

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
			throw new NotImplementedException();
		}
	}
}