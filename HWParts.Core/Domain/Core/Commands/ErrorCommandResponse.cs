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
                dynamic responseObj = new ExpandoObject();

                responseObj.status = "error";

                if (base.Result != null)
                {
                    responseObj.message = base.Result;

                    if (!string.IsNullOrEmpty(base.ResponseType))
                        responseObj.type = base.ResponseType;
                }

                responseObj.errors = Notifications.Select(x =>
                    {
                        dynamic obj = new ExpandoObject();
                        obj.description = x.Message;

                        if (x.Property != null)
                        {
                            obj.property = x.Property;
                        }

                        return obj;
                    });

                resultObj.response = responseObj;

                return resultObj;
            }
        }

        public ErrorCommandResponse()
        {

        }

        public ErrorCommandResponse(string result)
            : base(result)
        {

        }

        public ErrorCommandResponse(string responseType, string result)
            : base(responseType, result)
        {

        }
    }
}
