using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class PowerSupply : ComponentBase
    {
        public PowerSupply(
            string brand,
            string model,
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform)
            : base(brand, model, platformId, imageUrl, url, platform)
        {
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
