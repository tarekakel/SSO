using Domain.Entities;
using Domain.Interfaces.Generic;
using Domain.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Infrastructure.Db
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options,
       IHttpContextAccessor httpContextAccessor)
       : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SystemEntity> SystemEntities{ get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var user = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = now;
                    entry.Entity.CreatedBy = user;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = now;
                    entry.Entity.UpdatedBy = user;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var propertyMethod = Expression.Property(parameter, nameof(IAuditableEntity.IsDeleted));
                    var condition = Expression.Equal(propertyMethod, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            });

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        }


     

    }
}
