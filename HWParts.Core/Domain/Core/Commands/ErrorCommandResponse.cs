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

                if (base.Result != null)
                {
                    responseObj.message = base.Result;
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
    }
}
