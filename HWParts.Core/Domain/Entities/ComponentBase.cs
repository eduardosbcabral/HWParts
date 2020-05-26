using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class ComponentBase : EntityBase
    {
        public string Brand { get; protected set; }
        public string Model { get; protected set; }

        public string PlatformId { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public ComponentBase(
            string brand,
            string model,
            string platformId, 
            string imageUrl, 
            string url,
            EPlatform platform)
            : base()
        {
            Brand = brand;
            Model = model;

            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Platform = platform;
        }

        public ComponentBase(
            Guid id,
            string brand,
            string model,
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform)
            : base(id)
        {
            Brand = brand;
            Model = model;
            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Platform = platform;
        }
    }
}
