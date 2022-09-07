namespace WebApplication1.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var students = new List<Parking>
            {
            new Parking{ParkingID=1,Address="ul. nowa 21B",Size=22},
            new Parking{ParkingID=2,Address="ul. stara 21B",Size=23},
            new Parking{ParkingID=3,Address="ul. zielona 21B",Size=44},
            new Parking{ParkingID=4,Address="ul. czarna 21B",Size=11},
            new Parking{ParkingID=5,Address="ul. czerwona 21B",Size=21},
            new Parking{ParkingID=6,Address="ul. pomaranczowa 21B",Size=23},
            new Parking{ParkingID=7,Address="ul. belwederska 21B",Size=37},
            };

            students.ForEach(s => context.Parkings.Add(s));
            context.SaveChanges();

            var courses = new List<Bike>
            {
            new Bike{BikeID=1050,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1051,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1052,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1053,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1054,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1055,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{BikeID=1056,ParkingID=1,Type="elektryczny",Description="Rower elektryczny"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            new Bike{ParkingID=2,Type="miejski",Description="Rower miejski"},
            };
            courses.ForEach(s => context.Bikes.Add(s));
            context.SaveChanges();
        }
    }
}
