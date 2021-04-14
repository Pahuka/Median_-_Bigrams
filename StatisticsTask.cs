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
                .SelectMany(x => x.Where(y => y.Item1.SlideType == slideType && y.Item2.SlideType == slideType));

            if (tempValue.Count() != 0)
            {
                foreach (var item in tempValue)
                {
                    var minutes = item.Item2.DateTime - item.Item1.DateTime;
                    if ((minutes.TotalMinutes > 1.0) && (minutes.TotalMinutes < 120.0)) result.Add(minutes.TotalMinutes);
                }

                return result.Median();
            }

            return 0;
        }
    }
}