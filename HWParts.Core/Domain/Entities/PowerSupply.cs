using HWParts.Core.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HWParts.Core.Domain.Entities
{
    public class PowerSupply
    {
        [ForeignKey(nameof(ComponentBase))]
        public Guid Id { get; protected set; }
        public ComponentBase ComponentBase { get; private set; }
        public PowerSupply()
        {

        }
        public PowerSupply(
            string brand,
            string model,
            string platformId,
            string imageUrl,
            string url,
            EPlatform platform)
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
        }
    }
}
