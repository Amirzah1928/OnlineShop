using MediatR;

namespace OnlineShop.Application.Events.User.TrackingCode
{
    public class TrackingCodeCangeEvent : INotification
    {
        public List<string> TrackingCodes { get; set; } = [];
    }


    public static partial class EventMapper
    {
        public static TrackingCodeCangeEvent ToEvent(this List<string> trackingCodes)
        {
            return new TrackingCodeCangeEvent
            {
                TrackingCodes = trackingCodes,
            };
        }
    }
}
