using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Predictr.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any students.
            if (!context.Teams.Any())
            {
                var teams = new Team[]
            {
            new Team { Name = "Russia" },
                new Team { Name = "Egypt" },
                new Team { Name = "Saudi Arabia" },
                new Team { Name = "Uruguay" },
                new Team { Name = "Morocco" },
                new Team { Name = "Portugal" },
                new Team { Name = "Iran" },
                new Team { Name = "Spain" },
                new Team { Name = "France" },
                new Team { Name = "Argentina" },
                new Team { Name = "Australia" },
                new Team { Name = "Iceland" },
                new Team { Name = "Peru" },
                new Team { Name = "Croatia" },
                new Team { Name = "Denmark" },
                new Team { Name = "Nigeria" },
                new Team { Name = "Costa Rica" },
                new Team { Name = "Germany" },
                new Team { Name = "Serbia" },
                new Team { Name = "Mexico" },
                new Team { Name = "Brazil" },
                new Team { Name = "Sweden" },
                new Team { Name = "Switzerland" },
                new Team { Name = "South Korea" },
                new Team { Name = "Belgium" },
                new Team { Name = "Tunisia" },
                new Team { Name = "Panama" },
                new Team { Name = "England" },
                new Team { Name = "Columbia" },
                new Team { Name = "Poland" },
                new Team { Name = "Japan" },
                new Team { Name = "Senegal" }
            };
                foreach (Team t in teams)
                {
                    context.Teams.Add(t);
                }
                context.SaveChanges();
            }




            // group fixtures
            if (!context.Fixtures.Any()) {
                var fixtures = new Fixture[] {

                    new Fixture { Home="Russia", Away="Saudi Arabia", FixtureDateTime = Convert.ToDateTime("2018-06-14 16:00:00")}
                };

                foreach (Fixture f in fixtures)
                {
                    context.Fixtures.Add(f);
                }
                context.SaveChanges();
            }
        }
    }
}