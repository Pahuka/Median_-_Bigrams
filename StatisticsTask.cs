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
                .Select(x => x.OrderBy(t => t.DateTime))
                .Select(x => x.Bigrams())
                .SelectMany(x => x);

            if (tempValue.Count() != 0)
            {
                foreach (var item in tempValue)
                {
                    if (item.Item1.SlideType == slideType)
                    {
                        var minutes = item.Item2.DateTime - item.Item1.DateTime;
                        if ((minutes.TotalMinutes >= 1) && (minutes.TotalHours <= 2)) result.Add(minutes.TotalMinutes);
                    }
                }

                if (result.Count != 0) return result.Median();
            }

            return 0;
        }
    }
}