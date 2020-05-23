using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Domain.Commands.Admin.Motherboards
{
    public class ImportMotherboardsCommand : IRequest<bool>
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
