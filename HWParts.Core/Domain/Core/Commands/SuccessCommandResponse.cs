namespace HWParts.Core.Domain.Core.Commands
{
    public class SuccessCommandResponse : CommandResponse, ICommandResponse
    {
        public override object Result
        {
            get => new
            {
                Response = base.Result
            };
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
