using HWParts.Core.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Domain.ViewModels.Shared
{
    public class ComponentBaseViewModel : EntityBaseViewModel
    {
        [Display(Name = "ID da plataforma")]
        public string PlatformId { get; set; }

        [Display(Name = "URL da Imagem")]
        public string ImageUrl { get; set; }

        [Display(Name = "URL do componente")]
        public string Url { get; set; }

        [Display(Name = "Data do Crawler")]
        public DateTime CrawledAt { get; set; }

        [Display(Name = "Primeira vez disponível")]
        public DateTime FirstAvailable { get; set; }

        [Display(Name = "Plataforma")]
        public EPlatform Platform { get; set; }
    }
}
