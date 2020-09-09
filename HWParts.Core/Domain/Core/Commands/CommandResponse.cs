using Flunt.Notifications;
using System.Dynamic;

namespace HWParts.Core.Domain.Core.Commands
{
    public class CommandResponse : Notifiable, ICommandResponse
    {
        private object _result;
        public virtual object Result 
        {
            get
            {
                dynamic result = new ExpandoObject();

                if (Invalid)
                {
                    result.Errors = Notifications;
                }
                else
                {
                    result.Response = _result;
                }

                return result;
            }
            set => _result = value;
        }

        public CommandResponse()
        {

        }

        public CommandResponse(object result) => Result = result;
    }
}
