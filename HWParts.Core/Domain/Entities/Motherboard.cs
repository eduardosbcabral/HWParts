using HWParts.Core.Domain.Enums;
using System;

namespace HWParts.Core.Domain.Entities
{
    public class Motherboard : EntityBase
    {
        public string PlatformId { get; protected set; }
        public string Name { get; protected set; }
        public string Brand { get; protected set; }
        public string ProcessorBrand { get; protected set; }
        public string NumberOfMemorySlots { get; protected set; }
        public string MemoryStandard { get; protected set; }
        public string OnboardVideoChipset { get; protected set; }
        public string AudioChipset { get; protected set; }
        public string AudioChannels { get; protected set; }
        public string Model { get; protected set; }
        public string Item { get; protected set; }
        public decimal Price { get; protected set; }
        public string ImageUrl { get; protected set; }
        public string Url { get; protected set; }
        public EPlatform Platform { get; protected set; }

        public Motherboard(
            string platformId,
            string name,
            string brand,
            string processorBrand,
            string numberOfMemorySlots,
            string memoryStandard,
            string onboardVideoChipset,
            string audioChipset,
            string audioChannels,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            PlatformId = platformId;
            Name = name;
            Brand = brand;
            ProcessorBrand = processorBrand;
            NumberOfMemorySlots = numberOfMemorySlots;
            MemoryStandard = memoryStandard;
            OnboardVideoChipset = onboardVideoChipset;
            AudioChipset = audioChipset;
            AudioChannels = audioChannels;
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
            string numberOfMemorySlots,
            string memoryStandard,
            string onboardVideoChipset,
            string audioChipset,
            string audioChannels,
            string model,
            string item,
            decimal price,
            string imageUrl,
            string url)
        {
            PlatformId = platformId;
            Name = name;
            Brand = brand;
            NumberOfMemorySlots = numberOfMemorySlots;
            MemoryStandard = memoryStandard;
            OnboardVideoChipset = onboardVideoChipset;
            AudioChipset = audioChipset;
            AudioChannels = audioChannels;
            Model = model;
            Item = item;
            Price = price;
            ImageUrl = imageUrl;
            Url = url;
            UpdatedAt = DateTime.Now;
        }
    }
}
