namespace HomeIS.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HomeIS.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeIS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HomeIS.Models.ApplicationDbContext";
        }

        protected override void Seed(HomeIS.Models.ApplicationDbContext context)
        {
            var apartments = new List<Apartment>
            {
                new Apartment {
                    Balcony = true,
                    Description = "דירה מגניבה בתל אביב!",
                    FloorNumber = 8,
                    Images = new string[]{"https://lonelyplanetimages.imgix.net/a/g/hi/t/8ec64b64e1d0805b1101f6c70c7f5b31-tel-aviv.jpg?sharp=10&vib=20&w=1200",
                                "https://cdn.businesstraveller.com/wp-content/uploads/fly-images/879585/Tel-Aviv-916x515.jpg" },
                    Location = new Location {City = "תל אביב", Neighborhood = "רמת אביב", Address = "טאגור 43 תל אביב" },
                    NumberOfRooms = 6,
                    Owner = context.Users.Single(p => p.Email == "hayitai@gmail.com"),
                    PropertyValue = 500,
                    Size = 6
                } 
            };

            apartments.ForEach(s => context.Apartments.AddOrUpdate(p => p.Description, s));


        }
    }
}
