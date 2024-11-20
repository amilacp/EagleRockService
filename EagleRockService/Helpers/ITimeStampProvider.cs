namespace EagleRockService.Helpers
{
    public interface ITimeStampProvider
    {
        public DateTime GetCurrentDate();
        public DateTime GetCurrentDateTimeStamp();
        public DateTime GetCurrentDateTimeStampUtc();
    }
}