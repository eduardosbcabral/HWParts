namespace HWParts.Core.Domain.Commands
{
    public class ConfirmEmailAccount : AccountCommand
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public ConfirmEmailAccount()
        {

        }

        public ConfirmEmailAccount(string id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
