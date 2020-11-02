using HWParts.Core.Domain.Enums;

namespace HWParts.Core.Domain.Commands
{
    public class RegisterCaseCommand : CaseCommand
    {
        public string PlatformId { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public EPlatform Platform { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public RegisterCaseCommand(
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
        }
    }
}
