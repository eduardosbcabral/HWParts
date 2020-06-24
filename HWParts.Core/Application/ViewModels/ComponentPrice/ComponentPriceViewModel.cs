using HWParts.Core.Domain.Enums;

namespace HWParts.Core.Application.ViewModels.ComponentPrice
{
    public class ComponentPriceViewModel
    {
        public decimal Price { get; set; }
        public EPlatform Platform { get; set; }

        public ComponentPriceViewModel()
        {

        }
    }
}
