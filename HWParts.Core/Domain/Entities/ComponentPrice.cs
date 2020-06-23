using HWParts.Core.Domain.Core.Entities;
using HWParts.Core.Domain.Enums;

namespace HWParts.Core.Domain.Entities
{
    public class ComponentPrice : EntityBase
    {
        public decimal Price { get; private set; }
        public EPlatform Platform { get; private set; }
        public ComponentBase Component { get; private set; }

        public ComponentPrice()
        {

        }

        public ComponentPrice(decimal price, EPlatform platform, ComponentBase component)
            : base()
        {
            Price = price;
            Platform = platform;
            Component = component;
        }
    }
}
