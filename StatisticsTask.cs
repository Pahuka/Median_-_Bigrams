using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            var result = new List<double>();
            var tempValue = visits
                .GroupBy(userId => userId.UserId)
                .Select(x => x.Bigrams())
                .Select(x => x.Where(y => y.Item1.SlideType == slideType && y.Item2.SlideType == slideType))
                .SelectMany(x => x.Where(y => (y.Item2.DateTime - y.Item1.DateTime) >= TimeSpan.FromMinutes(1)
                || (y.Item2.DateTime - y.Item1.DateTime) <= TimeSpan.FromHours(2)));
            //.Select(x => (double)x.Item2.DateTime.Minute - x.Item1.DateTime.Minute);

            if (tempValue.Count() != 0)
            {
                foreach (var item in tempValue)
                {
                    var minutes = item.Item2.DateTime - item.Item1.DateTime;
                    result.Add(minutes.Minutes);
                }

                return result.Median();
            }

            return 0;
        }
    }
}