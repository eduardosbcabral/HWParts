using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Processor
{
    public class ImportProcessorsViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
