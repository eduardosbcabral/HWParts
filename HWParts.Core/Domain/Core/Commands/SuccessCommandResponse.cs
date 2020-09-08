namespace HWParts.Core.Domain.Core.Commands
{
    public class SuccessCommandResponse : CommandResponse, ICommandResponse
    {
        public SuccessCommandResponse()
        {

        }

        public SuccessCommandResponse(object result)
            : base(result)
        {

        }
    }
}
