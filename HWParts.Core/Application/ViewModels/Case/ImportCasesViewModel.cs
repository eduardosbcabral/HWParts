using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Case
{
    public class ImportCasesViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
