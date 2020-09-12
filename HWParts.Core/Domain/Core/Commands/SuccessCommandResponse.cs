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
                resultObj.status = "success";

                if (base.Result != null)
                {
                    resultObj.response = base.Result;
                }

                return resultObj;
            }
        }

        public SuccessCommandResponse()
        {

        }

        public SuccessCommandResponse(object result)
            : base(result)
        {

        }
    }
}
