using Flunt.Notifications;
using System.Dynamic;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable, ICommandResponse
    {
        public virtual object Result { get; set; }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;
    }
}
