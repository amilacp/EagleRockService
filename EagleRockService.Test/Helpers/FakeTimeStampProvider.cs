using EagleRockService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRockService.Test.Helpers
{
    public class FakeTimeStampProvider : ITimeStampProvider
    {
        public DateTime GetCurrentDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        public DateTime GetCurrentDateTimeStamp()
        {
            return new DateTime(2024, 11, 17, 18, 30, 2);
        }

        public DateTime GetCurrentDateTimeStampUtc()
        {
            return new DateTime(2024, 11, 17, 08, 30, 2);
        }
    }
}
