using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Infrastructure.Identity.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres..", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
