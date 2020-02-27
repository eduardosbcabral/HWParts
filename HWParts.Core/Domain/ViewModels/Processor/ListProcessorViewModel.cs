using HWParts.Core.Domain.Enums;

namespace HWParts.Core.Domain.ViewModels.Processor
{
    public class ListProcessorViewModel
    {
        public string Id { get; set; }
        public string PlatformId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Series { get; set; }
        public string L3Cache { get; set; }
        public string L2Cache { get; set; }
        public string CoolingDevice { get; set; }
        public string ManufacturingTech { get; set; }
        public string Model { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Platform { get; set; }
    }
}
