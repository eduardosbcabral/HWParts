using HWParts.Core.Application.ViewModels.Account;
using System.Threading.Tasks;

namespace HWParts.Core.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task Register(RegisterAccountViewModel registerAccountViewModel);
        Task Login(LoginAccountViewModel loginAccountViewModel);
    }
}
