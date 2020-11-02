using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;
using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HWParts.Core.Domain.Handlers
{
    public class RegisterCase : RequestHandler<RegisterCaseCommand, CommandResponse>
    {
        private readonly ICaseRepository _caseRepository;

        public RegisterCase(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        protected override CommandResponse Handle(RegisterCaseCommand request)
        {
            var caseEntity = new Case(
                request.Brand,
                request.Model,
                request.PlatformId,
                request.ImageUrl,
                request.Url,
                request.Platform);

            if (_caseRepository.Exists(caseEntity.PlatformId))
            {
                return new ErrorCommandResponse("Componente com o ID da plaforma existente.");
            }

            _caseRepository.Add(caseEntity);

            return new SuccessCommandResponse("Gabinete cadastrado com sucesso.", new
            {
                caseEntity.Id
            });
        }
    }
}
