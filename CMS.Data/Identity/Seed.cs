using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace CMS.Data.Identity
{
    public static class Seed
    {
        /// <summary>
        /// initialize Admin User data with associated Roles and AccessPaths
        /// </summary>
        /// <param name="context"></param>
        public static void Init(AuthContext context)
        {
            InitialData.GetAdminUsers().ForEach(u => context.Users.Add(u));

            InitialData.GetRoles().ForEach(r => context.Roles.AddOrUpdate(r));

            InitialData.GetUsersInRoles().ForEach(ur => context.Set<IdentityUserRole>().AddOrUpdate(ur));

            InitialData.GetAccessPathCategories().ForEach(apc => context.AccessPathCategories.AddOrUpdate(apc));

            var accessPaths = InitialData.GetAccessPaths();
            accessPaths.ForEach(ap => context.AccessPaths.AddOrUpdate(ap));

            InitialData.GetRolesAccessPaths(accessPaths)
                .ForEach(ra => context.RolesAccessPaths.AddOrUpdate(ra));
        }
    }
}