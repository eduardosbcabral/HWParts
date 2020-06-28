using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.ComponentPrice
{
    public class ComponentPriceViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [DisplayName("Preço")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "URL é obrigatória.")]
        [DisplayName("URL do produto")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Plataforma é obrigatória.")]
        [DisplayName("Plataforma")]
        public EPlatform Platform { get; set; }

        public Guid ComponentBaseId { get; set; }

        public ComponentPriceViewModel()
        {
        }

        public ComponentPriceViewModel(Guid componentBaseId)
        {
            ComponentBaseId = componentBaseId;
        }
    }
}
