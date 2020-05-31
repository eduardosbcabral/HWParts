using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Infrastructure.Identity.Models
{
    class UserRegistration
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres..", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A confirmação da senha não é igual.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string PasswordConfirm { get; set; }
    }
}
