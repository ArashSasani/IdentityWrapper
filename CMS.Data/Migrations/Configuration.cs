namespace CMS.Data.Migrations
{
    using CMS.Data.Identity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AuthContext context)
        {
            //Identity.Seed.Init(context);
        }
    }
}
