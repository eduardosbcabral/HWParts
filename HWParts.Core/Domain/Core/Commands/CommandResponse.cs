using Flunt.Notifications;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable
    {
        public string ResponseType { get; set; }
        public virtual object Result { get; set; }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;

        public CommandResponse(string responseType, object result)
        {
            ResponseType = responseType;
            Result = result;
        }
    }
}
