using Flunt.Notifications;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable
    {
        public virtual object Result { get; set; }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;
    }
}
