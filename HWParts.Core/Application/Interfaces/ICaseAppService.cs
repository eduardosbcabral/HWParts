using HWParts.Core.Domain.Commands;
using HWParts.Core.Domain.Core.Commands;

namespace HWParts.Core.Application.Interfaces
{
    public interface ICaseAppService
    {
        CommandResponse Register(RegisterCaseCommand command);
    }
}
