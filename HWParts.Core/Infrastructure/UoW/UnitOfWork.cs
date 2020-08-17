using HWParts.Core.Domain.Interfaces;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HWPartsDbContext _context;

        public UnitOfWork(HWPartsDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransactionAsync()
        {
            _context.Database.CommitTransaction();
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
