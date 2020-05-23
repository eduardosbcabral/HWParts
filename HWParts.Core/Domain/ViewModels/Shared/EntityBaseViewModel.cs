using System;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Domain.ViewModels
{
    public class EntityBaseViewModel
    {
        [Display(Name = "ID interno")]
        public string Id { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Última atualização")]
        public DateTime? UpdatedAt { get; set; }
    }
}
