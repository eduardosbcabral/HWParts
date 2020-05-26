using HWParts.Core.Domain.Interfaces;

namespace HWParts.Core.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HWPartsDbContext _context;

        public UnitOfWork(HWPartsDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
