using System.Threading.Tasks;

namespace HWParts.Core.Domain.Services
{
    public interface IService<T>
    {
        Task Execute();
    }
}
