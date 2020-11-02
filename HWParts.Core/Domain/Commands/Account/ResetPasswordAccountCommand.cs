namespace HWParts.Core.Domain.Commands
{
    public class ResetPasswordAccountCommand : AccountCommand
    {
        public string Code { get; set; }

        public ResetPasswordAccountCommand()
        {

        }
    }
}