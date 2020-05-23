using HWParts.Core.Domain.Enums;

namespace HWParts.Core.Domain.Commands.Shared
{
    public class ComponentBaseCommand : EntityBaseCommand
    {
        public string PlatformId { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public EPlatform Platform { get; set; }
    }
}
