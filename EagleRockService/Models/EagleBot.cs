namespace EagleRockService.Models
{
    public class EagleBot
    {
        public string EagleBotId { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public string RoadName { get; set; }
        public TrafficFlowDirection Direction { get; set; }
        public int TrafficFlowRate { get; set; }
        public double AverageSpeed { get; set; }
    }

}
