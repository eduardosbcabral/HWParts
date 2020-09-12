using System.Dynamic;

namespace HWParts.Core.Domain.Core.Commands
{
    public class SuccessCommandResponse : CommandResponse, ICommandResponse
    {
        public override object Result
        {
            get
            {
                dynamic resultObj = new ExpandoObject();

                if(base.Result != null)
                {
                    resultObj.data = base.Result;
                }

                resultObj.status = "success";

                if (!string.IsNullOrEmpty(Message))
                    resultObj.message = Message;

                return resultObj;
            }
        }

        public SuccessCommandResponse()
        {

        }

        public SuccessCommandResponse(string message, object result)
            : base(result, message)
        {
        }
    }
}
