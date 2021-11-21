using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using shul_board.Data.Base;
using shul_board.Models;
using System.Reflection;

namespace shul_board.Data
{
    public class ShulBoardContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ShulBoardContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        public DbSet<ScheduleGroup> ScheduleGroups { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IBaseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("Created").CurrentValue = DateTimeOffset.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property("LastUpdated").CurrentValue = DateTimeOffset.Now;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected virtual IList<Assembly> Assemblies
        {
            get
            {
                return new List<Assembly>()
                {
                    {
                        Assembly.Load("shul-board")
                    }
                };
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var assembly in Assemblies)
            {
                // Loads all types from an assembly which have an interface of IBase and is a public class
                var classes = assembly.GetTypes().Where(s => s.GetInterfaces().Any(_interface => _interface.Equals(typeof(IBaseEntity)) && s.IsClass && !s.IsAbstract && s.IsPublic));

                foreach (var _class in classes)
                {
                    // On Model Creating
                    var onModelCreatingMethod = _class.GetMethods().FirstOrDefault(x => x.Name == "OnModelCreating" && x.IsStatic);

                    if (onModelCreatingMethod != null)
                    {
                        onModelCreatingMethod.Invoke(_class, new object[] { builder });
                    }

                    // On Base Model Creating
                    if (_class.BaseType == null || _class.BaseType != typeof(BaseEntity))
                    {
                        continue;
                    }

                    var baseOnModelCreatingMethod = _class.BaseType.GetMethods().FirstOrDefault(x => x.Name == "OnModelCreating" && x.IsStatic);

                    if (baseOnModelCreatingMethod == null)
                    {
                        continue;
                    }

                    var baseOnModelCreatingGenericMethod = baseOnModelCreatingMethod.MakeGenericMethod(new Type[] { _class });

                    if (baseOnModelCreatingGenericMethod == null)
                    {
                        continue;
                    }

                    baseOnModelCreatingGenericMethod.Invoke(typeof(BaseEntity), new object[] { builder });
                }
            }
        }
    }
}