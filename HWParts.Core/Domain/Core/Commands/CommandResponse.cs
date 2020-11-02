using Flunt.Notifications;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable
    {
        public virtual object Result { get; set; }
        public string Message { get; set; }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;

        public CommandResponse(string message) => Message = message;

        public CommandResponse(object result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
