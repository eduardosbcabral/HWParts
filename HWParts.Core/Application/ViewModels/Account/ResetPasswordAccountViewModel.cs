namespace HWParts.Core.Application.ViewModels.Account
{
    public class ResetPasswordAccountViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public ResetPasswordAccountViewModel()
        {

        }

        public ResetPasswordAccountViewModel(string id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
