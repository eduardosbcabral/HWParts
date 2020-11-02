using HWParts.Core.Domain.Entities;
using HWParts.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace HWParts.Core.Infrastructure.Repositories
{
    public class CaseRepository : Repository<Case>, ICaseRepository
    {
        public CaseRepository(HWPartsDbContext context)
            : base(context)
        {
        }

        public bool Exists(string platformId)
        {
            return Db.Cases
                .Any(x => x.PlatformId == platformId);
        }
    }
}
