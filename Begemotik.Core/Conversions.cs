using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begemotik.Core
{
    public static class Conversions
    {
        /// <summary>
        /// Given start date, converts hijack period to finish date
        /// </summary>
        /// <param name="start"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static DateTime ToFinishDate(DateTime start, HashHijackPeriod period)
        {
            DateTime finishDate;
            switch (period)
            {
                case HashHijackPeriod.HalfAnfHour:
                    finishDate = start.AddMinutes(30);
                    break;
                case HashHijackPeriod.Hour:
                    finishDate = start.AddHours(1);
                    break;
                default:
                    throw new NotImplementedException("This HashHijackPeriod can't be converted to finish date.");
            }
            return finishDate;
        }
    }
}
