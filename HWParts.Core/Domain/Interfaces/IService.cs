using System.Threading.Tasks;

namespace HWParts.Core.Domain.Interfaces
{
    public interface IService<T>
    {
        Task Execute();
    }
}
