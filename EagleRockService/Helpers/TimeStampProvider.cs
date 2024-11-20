namespace EagleRockService.Helpers
{
    public class TimeStampProvider : ITimeStampProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }

        public DateTime GetCurrentDateTimeStamp()
        {
            return DateTime.Now;
        }

        public DateTime GetCurrentDateTimeStampUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
