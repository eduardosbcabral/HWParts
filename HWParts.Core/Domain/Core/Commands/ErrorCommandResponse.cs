using System.Dynamic;

namespace HWParts.Core.Domain.Core.Commands
{
    public class ErrorCommandResponse : CommandResponse, ICommandResponse
    {
        public override object Result
        {
            get => new
            {
                Response = new
                {
                    Message = base.Result,
                    Errors = Notifications
                }
            };
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
