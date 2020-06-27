using HWParts.Core.Application.ViewModels.Base;
using HWParts.Core.Application.ViewModels.ComponentPrice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Case
{
    public class CaseViewModel : ComponentBaseViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Marca")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Modelo")]
        public string Model { get; set; }

        public string FullName => $"{Brand} {Model}";
    }
}
