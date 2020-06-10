using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Account
{
    public class ForgotPasswordAccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
