using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Common
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity entity);
        void InsertAsync(TEntity entity);
        void Delete(object id);
        void DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
