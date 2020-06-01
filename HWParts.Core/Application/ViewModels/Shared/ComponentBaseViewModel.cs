using HWParts.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
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

        //[Required(ErrorMessage = "A URL da imagem é obrigatória.")]
        //[MinLength(2)]
        //[MaxLength(100)]
        //[DisplayName("URL da imagem")]
        public string ImageUrl => string.Join(";", ImagesUrls);

        [Required(ErrorMessage = "A URL da imagem é obrigatória.")]
        [MinLength(1, ErrorMessage = "É necessário incluir pelo menos uma url.")]
        [MaxLength(10, ErrorMessage = "O limite de imagens é 10.")]
        [DisplayName("URL's das imagens")]
        public IList<string> ImagesUrls { get; set; }

        [Required(ErrorMessage = "A URL do componente é obrigatória.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("URL do componente")]
        public string Url { get; set; }

        [Required(ErrorMessage = "A plataforma é obrigatória.")]
        [DisplayName("Plataforma")]
        public EPlatform Platform { get; set; }

        public ComponentBaseViewModel()
        {
            ImagesUrls = new List<string>();
        }
    }
}
