using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class Memory : EntityBase
    {
        public string PlatformId { get; protected set; }
        public string Name { get; protected set; }
        public string Brand { get; protected set; }
        public int Order { get; protected set; }
        public string CasLatency { get; protected set; }
        public string Voltage { get; protected set; }
        public string MultiChannelKit { get; protected set; }
        public string Timing { get; protected set; }
        public string HeatSpreader { get; protected set; }
        public string Model { get; protected set; }
        public string Item { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public Memory(
            string platformId,
            string name,
            string brand,
            int order,
            string casLatency,
            string voltage,
            string multiChannelKit,
            string timing,
            string heatSpreader,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            PlatformId = platformId;
            Name = name;
            Brand = brand;
            Order = order;
            CasLatency = casLatency;
            Voltage = voltage;
            MultiChannelKit = multiChannelKit;
            Timing = timing;
            HeatSpreader = heatSpreader;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;

            // New Product Default Values
            Platform = EPlatform.NewEgg;
        }

        public void Update(
            string platformId,
            string name,
            string brand,
            int order,
            string casLatency,
            string voltage,
            string multiChannelKit,
            string timing,
            string heatSpreader,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            PlatformId = platformId;
            Name = name;
            Brand = brand;
            Order = order;
            CasLatency = casLatency;
            Voltage = voltage;
            MultiChannelKit = multiChannelKit;
            Timing = timing;
            HeatSpreader = heatSpreader;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;

            UpdatedAt = DateTime.Now;
        }
    }
}
