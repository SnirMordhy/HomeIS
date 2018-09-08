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
            try
            {
                var apartments = new List<Apartment>
            {
                new Apartment {
                    Balcony = true,
                    Description = "דירה מגניבה בתל אביב!",
                    FloorNumber = 8,
                    PhotoList = new List<string>{"https://lonelyplanetimages.imgix.net/a/g/hi/t/8ec64b64e1d0805b1101f6c70c7f5b31-tel-aviv.jpg?sharp=10&vib=20&w=1200",
                                "https://cdn.businesstraveller.com/wp-content/uploads/fly-images/879585/Tel-Aviv-916x515.jpg" },
                    Location = new Location {City = "תל אביב", Neighborhood = "רמת אביב", Address = "טאגור 43 תל אביב" },
                    NumberOfRooms = 6,
                    Owner = context.Users.Single(p => p.Email == "hayitai@gmail.com"),
                    PropertyValue = 500,
                    Size = 6
                },
                new Apartment {
                    Balcony = true,
                    Description = "Walt Disneys castle!",
                    FloorNumber = 8,
                    PhotoList = new List<string>{"https://imagesvc.timeincapp.com/v3/mm/image?url=https%3A%2F%2Fcdn-image.travelandleisure.com%2Fsites%2Fdefault%2Ffiles%2F1490891161%2Fcinderella-castle-disneyworld-DISNEYCASTLE0317.jpg&w=700&q=85",
                                "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSlz6y4j5vNDBxS4lMyVVNtqXKco-dNON9WUK41EY6shDk1r_GM1g" },
                    Location = new Location {City = "Florida", Neighborhood = "Orlando", Address = "Disney Orlando" },
                    NumberOfRooms = 5,
                    Owner = context.Users.Single(p => p.Email == "hayitai@gmail.com"),
                    PropertyValue = 10000,
                    Size = 330000
                }
            };

                apartments.ForEach(s => context.Apartments.AddOrUpdate(p => p.Description, s));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);

            }


        }
    }
}
