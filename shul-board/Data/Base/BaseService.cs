using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data.Base
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
                where TEntity : class, IBaseEntity
    {
        protected ShulBoardContext _context;

        protected BaseService([NotNull] ShulBoardContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetAsync(id);

            return !(entity == null);
        }

        public virtual async Task<TEntity> GetAsync(int id, bool track = true)
        {
            var query = this.GetAllQuery(track);

            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool track)
        {
            var query = this.GetAllQuery(track);

            return await  query.ToListAsync();
        }

        public virtual IQueryable<TEntity> GetAllQuery(bool track)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (!track) query = query.AsNoTracking();
            return query;
        }


        public virtual async Task<TEntity> UpdateAsync(int id, TEntity updateEntity)
        {
            // Check that the record exists.
            var entity = await GetAsync(id);

            if (entity == null)
            {
                throw new Exception("Unable to find record with id '" + id + "'.");
            }

            // Update changes if any of the properties have been modified.
            _context.Entry(entity).CurrentValues.SetValues(updateEntity);
            _context.Entry(entity).State = EntityState.Modified;

            if (_context.Entry(entity).Properties.Any(property => property.IsModified))
            {
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            // Check that the record exists.
            var entity = await GetAsync(id);

            if (entity == null)
            {
                throw new Exception("Unable to find record with id '" + id + "'.");
            }

            _context.Remove(entity);

            // Save changes to the Db Context.
            await _context.SaveChangesAsync();
        }


    }
}
