using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class Motherboard : ComponentBase
    {
        public string Brand { get; protected set; }
        public string Model { get; protected set; }

        public Motherboard(
            string brand,
            string model,
            string platformId,
            string imageUrl,
            string url,
            int order,
            EPlatform platform)
            : base(platformId, imageUrl, url, order, platform)
        {
            Brand = brand;
            Model = model;
        }

        public void Update(
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform,
            string brand,
            string model)
        {
            PlatformId = platformId;
            ImageUrl = imageUrl;
            Url = url;
            Platform = platform;
            Brand = brand;
            Model = model;

            UpdatedAt = DateTime.Now;
        }
    }
}
