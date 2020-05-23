using HWParts.Core.Domain.Commands.Shared;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using MediatR;

namespace HWParts.Core.Domain.Commands.Admin.Motherboards
{
    public class EditMotherboardCommand : ComponentBaseCommand, IRequest<EditMotherboardViewModel>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CpuSocketType { get; set; }
        public string CpuType { get; set; }
        public string Chipset { get; set; }
        public string NumberOfMemorySlots { get; set; }
        public string MemoryStandard { get; set; }
        public string MaximumMemorySupported { get; set; }
        public string ChannelSupported { get; set; }
        public string PciExpress30x16 { get; set; }
        public string PciExpressx1 { get; set; }
        public string Sata6Gbs { get; set; }
        public string M2 { get; set; }
        public string OnboardVideoChipset { get; set; }
        public string AudioChipset { get; set; }
        public string AudioChannels { get; set; }
        public string LanChipset { get; set; }
        public string MaxLanSpeed { get; set; }
        public string BackIOPorts { get; set; }
        public string OnboardUsb { get; set; }
        public string OtherConnectors { get; set; }
        public string FormFactor { get; set; }
        public string Dimensions { get; set; }
        public string PowerPin { get; set; }

        public EditMotherboardCommand(string brand,
            string model,
            string cpuSocketType,
            string cpuType,
            string chipset,
            string numberOfMemorySlots,
            string memoryStandard,
            string maximumMemorySupported,
            string channelSupported,
            string pciExpress30x16,
            string pciExpressx1,
            string sata6Gbs,
            string m2,
            string onboardVideoChipset,
            string audioChipset,
            string audioChannels,
            string lanChipset,
            string maxLanSpeed,
            string backIOPorts,
            string onboardUsb,
            string otherConnectors,
            string formFactor,
            string dimensions,
            string powerPin)
        {
            Brand = brand;
            Model = model;
            CpuSocketType = cpuSocketType;
            CpuType = cpuType;
            Chipset = chipset;
            NumberOfMemorySlots = numberOfMemorySlots;
            MemoryStandard = memoryStandard;
            MaximumMemorySupported = maximumMemorySupported;
            ChannelSupported = channelSupported;
            PciExpress30x16 = pciExpress30x16;
            PciExpressx1 = pciExpressx1;
            Sata6Gbs = sata6Gbs;
            M2 = m2;
            OnboardVideoChipset = onboardVideoChipset;
            AudioChipset = audioChipset;
            AudioChannels = audioChannels;
            LanChipset = lanChipset;
            MaxLanSpeed = maxLanSpeed;
            BackIOPorts = backIOPorts;
            OnboardUsb = onboardUsb;
            OtherConnectors = otherConnectors;
            FormFactor = formFactor;
            Dimensions = dimensions;
            PowerPin = powerPin;
        }
    }
}
