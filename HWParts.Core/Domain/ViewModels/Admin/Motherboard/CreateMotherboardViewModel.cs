using HWParts.Core.Domain.ViewModels.Shared;
using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Domain.ViewModels.Admin.Motherboard
{
    public class CreateMotherboardViewModel : ComponentBaseViewModel
    {
        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Display(Name = "Tipo de soquete")]
        public string CpuSocketType { get; set; }

        [Display(Name = "Tipo da CPU")]
        public string CpuType { get; set; }

        [Display(Name = "Chipset")]
        public string Chipset { get; set; }

        [Display(Name = "Quantidade de slots de memória")]
        public string NumberOfMemorySlots { get; set; }

        [Display(Name = "Padrão da memória")]
        public string MemoryStandard { get; set; }

        [Display(Name = "Quantidade máxima de memória")]
        public string MaximumMemorySupported { get; set; }

        [Display(Name = "Canais suportados")]
        public string ChannelSupported { get; set; }

        [Display(Name = "PCI Express 3.0 x16")]
        public string PciExpress30x16 { get; set; }

        [Display(Name = "PCI Express x1")]
        public string PciExpressx1 { get; set; }

        [Display(Name = "Sata 6Gbs")]
        public string Sata6Gbs { get; set; }

        [Display(Name = "M.2")]
        public string M2 { get; set; }

        [Display(Name = "Chipset da placa de vídeo onboard")]
        public string OnboardVideoChipset { get; set; }

        [Display(Name = "Chipset de áudio")]
        public string AudioChipset { get; set; }

        [Display(Name = "Canais de áudio")]
        public string AudioChannels { get; set; }

        [Display(Name = "Chipset da LAN")]
        public string LanChipset { get; set; }

        [Display(Name = "Velocidade máxima da LAN")]
        public string MaxLanSpeed { get; set; }

        [Display(Name = "Portas I/O traseiras")]
        public string BackIOPorts { get; set; }

        [Display(Name = "USB Onboard")]
        public string OnboardUsb { get; set; }

        [Display(Name = "Outros conectores")]
        public string OtherConnectors { get; set; }

        [Display(Name = "Padrão")]
        public string FormFactor { get; set; }

        [Display(Name = "Tamanho")]
        public string Dimensions { get; set; }

        [Display(Name = "Pinos de energia")]
        public string PowerPin { get; set; }
    }
}
