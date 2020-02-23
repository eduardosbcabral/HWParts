using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class Processor : EntityBase
    {
        public string PlatformId { get; protected set; }
        public string Name { get; protected set; }
        public string Brand { get; protected set; }
        public string Series { get; protected set; }
        public string L3Cache { get; protected set; }
        public string L2Cache { get; protected set; }
        public string CoolingDevice { get; protected set; }
        public string ManufacturingTech { get; protected set; }
        public string Model { get; protected set; }
        public string Item { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public Processor(
            string platformId,
            string name,
            string brand,
            string series,
            string l3Cache,
            string l2Cache,
            string coolingDevice,
            string manufacturingTech,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            PlatformId = platformId;
            Name = name;
            Brand = brand;
            Series = series;
            L3Cache = l3Cache;
            L2Cache = l2Cache;
            CoolingDevice = coolingDevice;
            ManufacturingTech = manufacturingTech;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;

            // New Product Default Values
            Platform = EPlatform.NewEgg;
        }

        public void Update(
            string name,
            string brand,
            string series,
            string l3Cache,
            string l2Cache,
            string coolingDevice,
            string manufacturingTech,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            Name = name;
            Brand = brand;
            Series = series;
            L3Cache = l3Cache;
            L2Cache = l2Cache;
            CoolingDevice = coolingDevice;
            ManufacturingTech = manufacturingTech;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;
            UpdatedAt = DateTime.Now;
        }
    }
}
