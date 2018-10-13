namespace HomeIS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HomeIS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeIS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HomeIS.Models.ApplicationDbContext";
        }

        protected override void Seed(HomeIS.Models.ApplicationDbContext context)
        {
            // add application roles
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists("Admin"))
            {
                var roleResult = RoleManager.Create(new IdentityRole("Admin"));
            }
            if (!RoleManager.RoleExists("User"))
            {
                var roleResult = RoleManager.Create(new IdentityRole("User"));
            }
        }
    }
}
