using Y.EventBus;

namespace Y.Chat.EntityCore.Domain.UserDomain.Events
{
    [EventDiscriptor("SendEmail")]
    public class SendEmailEvent
    {
        public string Email { get; set; }

        public string Numbers { get; set; }
    }
}