using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Account
{
    public class ResetPasswordAccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Id { get; set; }
        public string Code { get; set; }

        public ResetPasswordAccountViewModel()
        {

        }

        public ResetPasswordAccountViewModel(string code)
        {
            Code = code;
        }

        public ResetPasswordAccountViewModel(string id, string code)
        {
            Id = id;
            Code = code;
        }
    }
}
