using BSK_Projekt2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.App_Start
{
    public class AppContext : IdentityDbContext
    {
        public AppContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<AppContext>());
            TableClearances = new List<EntityClearance>();
            SeedClearances();
        }
        public static AppContext create()
        {
            return new AppContext();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> UsersTable { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public List<EntityClearance> TableClearances { get; set; }

        private void SeedClearances()
        {
            TableClearances.Add(new EntityClearance(create:1,read: 1,update: 1,delete: 2)
            {
                EntityName = "cars"
            });
            TableClearances.Add(new EntityClearance(create: 2, read:2, update: 2, delete: 3)
            {
                EntityName = "repairs"
            });
            TableClearances.Add(new EntityClearance(create: 3, read: 3, update: 3, delete: 3)
            {
                EntityName = "invoices"
            });
        }

    }
    public class EntityClearance
    {

        public EntityClearance()
        {
            Classification = new Dictionary<string, int>();
        }
        public EntityClearance(int create, int read, int update, int delete)
        {
            Classification = new Dictionary<string, int>
            {
                { "create", create },
                { "read", read },
                { "update", update },
                { "delete", delete }
            };

        }
        public string EntityName { get; set; }
        public Dictionary<string, int> Classification { get; set; }

        public bool CanDoIt(string operation, User user)
        {
            if (operation == "read")
            {
                if (user.Clearance >= this.Classification[operation]) return true; //no read up
                else return false;
            }
            else if (user.Clearance > this.Classification[operation]) return false; //no write down
            else return true;
        }
    }
}