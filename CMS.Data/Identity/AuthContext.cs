using CMS.Core.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CMS.Data.Identity
{

    public class AuthContext : IdentityDbContext<User, IdentityRole, string, IdentityUserLogin
        , IdentityUserRole, IdentityUserClaim>
    {
        public AuthContext() : base("UberContext")
        {
            Database.SetInitializer(new AuthContextInitializer());
        }

        public DbSet<AccessPathCategory> AccessPathCategories { get; set; }
        public DbSet<AccessPath> AccessPaths { get; set; }
        public DbSet<RoleAccessPath> RolesAccessPaths { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<RestrictedIP> RestrictedIPs { get; set; }
        public DbSet<RestrictedAccessTime> RestrictedAccessTimes { get; set; }

        public static AuthContext Create()
        {
            return new AuthContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region table naming conventions
            modelBuilder.Entity<User>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UsersInRoles", "security");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "security");
            modelBuilder.Entity<RoleAccessPath>().ToTable("RolesAccessPaths", "security");
            modelBuilder.Entity<AccessPath>().ToTable("AccessPaths", "security");
            modelBuilder.Entity<AccessPathCategory>().ToTable("AccessPathCategories", "security");
            modelBuilder.Entity<UserLog>().ToTable("UserLogs", "security");
            modelBuilder.Entity<RestrictedIP>().ToTable("RestrictedIPs", "security");
            modelBuilder.Entity<RestrictedAccessTime>().ToTable("RestrictedAccessTimes", "security");
            #endregion
        }
    }

    public class AuthContextInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            Identity.Seed.Init(context);

            base.Seed(context);
        }
    }
}