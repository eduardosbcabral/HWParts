using HWParts.Core.Domain.Entities;

namespace HWParts.Core.Domain.Interfaces
{
    public interface ICaseRepository : IRepository<Case>
    {
        bool Exists(string platformId);
    }
}
