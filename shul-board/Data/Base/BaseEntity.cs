using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }

        public static void OnModelCreating<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, IBaseEntity
        {
            modelBuilder.Entity<TEntity>().HasKey(entity => entity.Id);
        }
    }
}
