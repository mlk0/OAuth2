namespace MvcOAuth2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MvcOAuth2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcOAuth2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcOAuth2.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private bool AddUserAndRole(MvcOAuth2.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole> (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));


            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };


            ir = um.Create(user, "P_assw0rd1");

            if (ir.Succeeded == false)
                return ir.Succeeded;

            ir = um.AddToRole(user.Id, "canEdit");

            return ir.Succeeded;
        }
    }
}
