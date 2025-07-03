namespace OnlineShop
{
    public class Settings
    {
        public required TrackingCodeSettings TrackingCode { get; set; }
        public required PriorityConfigSettings PriorityConfig { get; set; }
    }

    public class TrackingCodeSettings
    {
        public required string BaseURL { get; set; }
        public required string GetURL { get; set; }
        public required string Prefix { get; set; }
    }


    public class PriorityConfigSettings
    {
        public int TrackingCodeProxy { get; set; }
        public int LocalTrackingCodeProxy { get; set; }
    }
}
