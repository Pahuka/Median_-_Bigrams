using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            var temp = DateTime.Now - TimeSpan.FromMinutes(40);
            if (visits.Count > 1)
            {
                var t = visits
                    .GroupBy(userId => userId.UserId)
                    .Where(key => key.Select(time => time.DateTime) > TimeSpan.FromMinutes(1));

                //return visits
                //    .GroupBy(userId => userId.UserId)
                //    .Select(time => time.Select(c => (double)c.DateTime.Minute).Bigrams())
                //    .Select(x => x.Select(y => y.Item2 - y.Item1))
                //    .SelectMany(x => x)
                //    .Median();
            }

            return 0;
        }
    }
}