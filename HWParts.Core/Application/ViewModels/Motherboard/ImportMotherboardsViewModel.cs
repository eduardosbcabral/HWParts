using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Motherboard
{
    public class ImportMotherboardsViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
