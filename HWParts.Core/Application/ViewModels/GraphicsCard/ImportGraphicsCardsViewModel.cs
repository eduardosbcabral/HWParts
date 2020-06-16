using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Application.ViewModels.GraphicsCard
{
    public class ImportGraphicsCardsViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
