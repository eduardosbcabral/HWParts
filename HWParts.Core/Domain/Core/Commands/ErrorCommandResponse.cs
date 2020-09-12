using System.Dynamic;
using System.Linq;

namespace HWParts.Core.Domain.Core.Commands
{
    public class ErrorCommandResponse : CommandResponse, ICommandResponse
    {
        public override object Result
        {
            get
            {
                dynamic resultObj = new ExpandoObject();
                resultObj.status = "error";

                if (!string.IsNullOrEmpty(Message))
                    resultObj.message = Message;

                resultObj.errors = Notifications.Select(x =>
                    {
                        dynamic obj = new ExpandoObject();
                        obj.description = x.Message;

                        if (x.Property != null)
                        {
                            obj.property = x.Property;
                        }

                        return obj;
                    });

                return resultObj;
            }
        }

        public ErrorCommandResponse()
        {

        }

        public ErrorCommandResponse(string message)
            : base(message)
        {
        }
    }
}
