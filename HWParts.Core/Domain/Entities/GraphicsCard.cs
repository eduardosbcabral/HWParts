using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class GraphicsCard : EntityBase
    {
        public string PlatformId { get; protected set; }
        public string Name { get; protected set; }
        public string Brand { get; protected set; }
        public int Order { get; protected set; }
        public string ChipsetManufacturer { get; protected set; }
        public string CoreClock { get; protected set; }
        public string MaxResolution { get; protected set; }
        public string DisplayPort { get; protected set; }
        public string Hdmi { get; protected set; }
        public string Dvi { get; protected set; }
        public string CardDimensions { get; protected set; }
        public string Model { get; protected set; }
        public string Item { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public GraphicsCard(
            string platformId,
            string name,
            string brand,
            int order,
            string chipsetManufacturer,
            string coreClock,
            string maxResolution,
            string displayPort,
            string hdmi,
            string dvi,
            string cardDimensions,
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
            ChipsetManufacturer = chipsetManufacturer;
            CoreClock = coreClock;
            MaxResolution = maxResolution;
            DisplayPort = displayPort;
            Hdmi = hdmi;
            Dvi = dvi;
            CardDimensions = cardDimensions;
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
            string chipsetManufacturer,
            string coreClock,
            string maxResolution,
            string displayPort,
            string hdmi,
            string dvi,
            string cardDimensions,
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
            ChipsetManufacturer = chipsetManufacturer;
            CoreClock = coreClock;
            MaxResolution = maxResolution;
            DisplayPort = displayPort;
            Hdmi = hdmi;
            Dvi = dvi;
            CardDimensions = cardDimensions;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;

            UpdatedAt = DateTime.Now;
        }
    }
}
