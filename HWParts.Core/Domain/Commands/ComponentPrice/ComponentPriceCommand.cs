using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Commands
{
    public abstract class ComponentPriceCommand : Command
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }
        public EPlatform Platform { get; set; }

        public Guid ComponentBaseId { get; set; }
    }
}
