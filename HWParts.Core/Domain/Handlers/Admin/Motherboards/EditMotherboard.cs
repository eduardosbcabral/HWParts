using AutoMapper;
using HWParts.Core.Domain.Commands.Admin.Motherboards;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Repositories;
using HWParts.Core.Domain.ViewModels.Admin.Motherboard;
using HWParts.Core.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers.Admin.Motherboards
{
    public class EditMotherboard : IRequestHandler<EditMotherboardCommand, EditMotherboardViewModel>
    {
        private readonly HWPartsDbContext _context;
        private readonly IMotherboardRepository _motherboardRepository;
        private readonly IMapper _mapper;

        public EditMotherboard(
            HWPartsDbContext context,
            IMotherboardRepository motherboardRepository,
            IMapper mapper)
        {
            _context = context;
            _motherboardRepository = motherboardRepository;
            _mapper = mapper;
        }

        public async Task<EditMotherboardViewModel> Handle(EditMotherboardCommand command, CancellationToken cancellationToken)
        {
            var motherboard = await _motherboardRepository.GetByIdAsync(command.Id);

            motherboard.Update(
                command.PlatformId,
                command.ImageUrl,
                command.Url,
                command.Platform,
                command.Brand,
                command.Model);

            _context.Update(motherboard);

            await _context.SaveChangesAsync();

            return _mapper.Map<Motherboard, EditMotherboardViewModel>(motherboard);
        }
    }
}
