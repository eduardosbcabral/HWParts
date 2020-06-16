using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.PowerSupply
{
    public class ImportPowerSuppliesViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
