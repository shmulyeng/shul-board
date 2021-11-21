using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data.Base
{
    public interface IBaseService<TEntity>
                 where TEntity : class, IBaseEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> GetAsync(int id, bool track);

        Task<bool> ExistsAsync(int id);

        Task<TEntity> UpdateAsync(int id, TEntity updateEntity);

        Task DeleteAsync(int id);
    }

}
