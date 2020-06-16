using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.Storage
{
    public class ImportStoragesViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
