using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Commands
{
    public abstract class ProcessorCommand : Command
    {
        // EntityBase
        public Guid Id { get; set; }

        // ComponentBase
        public string PlatformId { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public EPlatform Platform { get; set; }

        // Motherboard
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
