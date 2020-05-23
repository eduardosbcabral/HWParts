using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class ComponentBase : EntityBase
    {
        public string PlatformId { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public int Order { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public ComponentBase(
            string platformId, 
            string imageUrl, 
            string url, 
            int order,
            EPlatform platform)
        {
            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Order = order;
            Platform = platform;
        }
    }
}
