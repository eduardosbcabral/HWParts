namespace HWParts.Core.Domain.Core.Commands
{
    public class ErrorCommandResponse : CommandResponse, ICommandResponse
    {
        public ErrorCommandResponse()
        {

        }

        public ErrorCommandResponse(object result)
            : base(result)
        {

        }
    }
}
