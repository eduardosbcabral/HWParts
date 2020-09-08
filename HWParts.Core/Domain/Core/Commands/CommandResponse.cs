using Flunt.Notifications;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable, ICommandResponse
    {
        public object Result { get; }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;
    }
}
