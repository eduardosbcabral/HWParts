using System.ComponentModel.DataAnnotations;

namespace HWParts.Core.Domain.ViewModels.Admin.Motherboard
{
    public class ListMotherboardViewModelAdmin
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        [Display(Description = "Nome")]
        public string Name => $"{Brand} {Model}";
    }
}
