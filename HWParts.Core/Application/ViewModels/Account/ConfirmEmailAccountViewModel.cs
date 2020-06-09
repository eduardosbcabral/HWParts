namespace HWParts.Core.Application.ViewModels.Account
{
    public class ConfirmEmailAccountViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }

        public ConfirmEmailAccountViewModel()
        {

        }

        public ConfirmEmailAccountViewModel(string id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
