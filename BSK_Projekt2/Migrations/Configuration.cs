namespace BSK_Projekt2.Migrations
{
    using BSK_Projekt2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BSK_Projekt2.App_Start.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BSK_Projekt2.App_Start.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Cars.RemoveRange(context.Cars);
            context.Invoices.RemoveRange(context.Invoices);
            context.Repairs.RemoveRange(context.Repairs);
            context.Set<Repair>().RemoveRange(context.Set<Repair>());
            context.Set<Invoice>().RemoveRange(context.Set<Invoice>());
            context.Set<Car>().RemoveRange(context.Set<Car>());

            SeedUsers(context);
            SeedCars(context);
            SeedInvoices(context);
            SeedRepairs(context);
            
        }
        private void SeedUsers(BSK_Projekt2.App_Start.AppContext context)
        {
            if (!context.UsersTable.Any(u => u.UserName == "wlasciciel"))
            {
                var user = new User()
                {
                    UserName = "wlasciciel",
                    Password = (new PasswordHasher()).HashPassword("wlasciciel"),
                    Clearance = 3
                };
                context.UsersTable.Add(user);

            }
            if (!context.UsersTable.Any(u => u.UserName == "mechanik"))
            {
                var user = new User() {
                    UserName = "mechanik",
                    Password = (new PasswordHasher()).HashPassword("mechanik"),
                    Clearance = 2
                };
                context.UsersTable.Add(user);
            }
            if (!context.UsersTable.Any(u => u.UserName == "klient"))
            {
                var user = new User() {
                    UserName = "klient",
                    Password = (new PasswordHasher()).HashPassword("klient"),
                    Clearance = 1
                };
                context.UsersTable.Add(user);
            }
            context.SaveChanges();
        }

        private void SeedCars(BSK_Projekt2.App_Start.AppContext context)
        {
            var user = context.UsersTable.Where(u => u.UserName == "klient").FirstOrDefault();
            var car = new Car()
            {
                Model = "Astra",
                Brand = "Opel"
            };
            user.Cars.Add(car);
            context.Cars.Add(car);

            car = new Car()
            {
                Model = "Corsa",
                Brand = "Opel"
            };
            context.Cars.Add(car);
            user.Cars.Add(car);
            car = new Car()
            {
                Model = "Omega",
                Brand = "Opel"
            };
            context.Cars.Add(car);
            user.Cars.Add(car);
            context.SaveChanges();
        }
        private void SeedRepairs(BSK_Projekt2.App_Start.AppContext context)
        {
            var mechanik = context.UsersTable.Where(u => u.UserName == "mechanik").FirstOrDefault();
            var car = context.Cars.Where(c => c.Id == 1).FirstOrDefault();
            var repair = new Repair()
            {
                Description = "Naprawiono silnik",
                Cost = 2000,
                RepairedCar = car,
                Mechanic = mechanik
            };
            car.Repairs.Add(repair);
            mechanik.Repairs.Add(repair);
            context.Repairs.Add(repair);

            car = context.Cars.Where(c => c.Id == 2).FirstOrDefault();
            repair = new Repair()
            {
                Description = "Naprawiono ko³o",
                Cost = 100,
                RepairedCar = context.Cars.Where(c => c.Id == 2).FirstOrDefault(),
                Mechanic = mechanik
            };
            car.Repairs.Add(repair);
            mechanik.Repairs.Add(repair);
            context.Repairs.Add(repair);


            repair = new Repair()
            {
                Description = "Wymieniono filtry i olej",
                Cost =100,
                RepairedCar = context.Cars.Where(c => c.Id == 2).FirstOrDefault(),
                Mechanic = mechanik
            };
            car.Repairs.Add(repair);
            mechanik.Repairs.Add(repair);
            context.Repairs.Add(repair);

            car = context.Cars.Where(c => c.Id == 3).FirstOrDefault();
            repair = new Repair()
            {
                Description = "Wymieniono klocki hamulcowe",
                Cost = 500,
                RepairedCar = context.Cars.Where(c => c.Id == 3).FirstOrDefault(),
                Mechanic = mechanik
            };
            car.Repairs.Add(repair);
            mechanik.Repairs.Add(repair);
            context.Repairs.Add(repair);

            context.SaveChanges();
        }

        private void SeedInvoices(BSK_Projekt2.App_Start.AppContext context)
        {
            var owner = context.UsersTable.Where(u => u.UserName == "wlasciciel").FirstOrDefault();
            var car = context.Cars.Where(c => c.Id == 1).FirstOrDefault();

            var invoice = new Invoice()
            {
                Cost = 2500,
                Issuer = owner,
                Repairs = car.Repairs
            };
            context.Invoices.Add(invoice);

            car = context.Cars.Where(c => c.Id == 2).FirstOrDefault();
            invoice = new Invoice()
            {
                Cost = 300,
                Issuer = owner,
                Repairs = car.Repairs
            };
            context.Invoices.Add(invoice);

            car = context.Cars.Where(c => c.Id == 3).FirstOrDefault();
            invoice = new Invoice()
            {
                Cost = 800,
                Issuer = owner,
                Repairs = car.Repairs
            };
            context.Invoices.Add(invoice);

            context.SaveChanges();
        }

    }

}
