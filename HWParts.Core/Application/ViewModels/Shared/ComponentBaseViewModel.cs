using HWParts.Core.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Base
{
    public class ComponentBaseViewModel
    {
        [Required(ErrorMessage = "O ID da plataforma é obrigatório.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("ID da plataforma")]
        public string PlatformId { get; set; }

        [Required(ErrorMessage = "A URL da imagem é obrigatória.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("URL da imagem")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "A URL do componente é obrigatória.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("URL do componente")]
        public string Url { get; set; }

        [Required(ErrorMessage = "A plataforma é obrigatória.")]
        [DisplayName("Plataforma")]
        public EPlatform Platform { get; set; }
    }
}
