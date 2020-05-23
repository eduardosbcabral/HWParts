using HWParts.Core.Domain.Commands.Shared;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using MediatR;

namespace HWParts.Core.Domain.Commands.Admin.Motherboards
{
    public class CreateMotherboardCommand : ComponentBaseCommand, IRequest<CreateMotherboardViewModel>
    {
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
