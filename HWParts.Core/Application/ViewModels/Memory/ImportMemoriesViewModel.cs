using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Memory
{
    public class ImportMemoriesViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
