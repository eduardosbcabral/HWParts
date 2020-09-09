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
                dynamic responseObj = new ExpandoObject();
                if(base.Result != null)
                {
                    responseObj.Message = base.Result;
                }

                responseObj.Errors = Notifications.Select(x =>
                    {
                        dynamic obj = new ExpandoObject();
                        obj.description = x.Message;

                        if (x.Property != null)
                        {
                            obj.property = x.Property;
                        }

                        return obj;
                    });

                return responseObj;
            }
        }

        public ErrorCommandResponse()
        {

        }

        public ErrorCommandResponse(string result)
            : base(result)
        {

        }
    }
}
