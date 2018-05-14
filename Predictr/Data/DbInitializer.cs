using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // create the two roles

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any())
            {
                roleStore.CreateAsync(new IdentityRole("Admin"));
                roleStore.CreateAsync(new IdentityRole("Player"));
            }
            

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

                    new Fixture { Group="A", Home="Russia", Away="Saudi Arabia", FixtureDateTime = Convert.ToDateTime("2018-06-14 16:00:00")},
                    
                    new Fixture { Group="A", Home="Egypt", Away="Uruguay", FixtureDateTime = Convert.ToDateTime("2018-06-15 13:00:00")},
                    new Fixture { Group="B", Home="Morocco", Away="Iran", FixtureDateTime = Convert.ToDateTime("2018-06-15 16:00:00")},
                    new Fixture { Group="B", Home="Portugal", Away="Spain", FixtureDateTime = Convert.ToDateTime("2018-06-15 19:00:00")},

                    new Fixture { Group="C", Home="France", Away="Australia", FixtureDateTime = Convert.ToDateTime("2018-06-16 11:00:00")},
                    new Fixture { Group="D", Home="Argentina", Away="Iceland", FixtureDateTime = Convert.ToDateTime("2018-06-16 14:00:00")},
                    new Fixture { Group="C", Home="Peru", Away="Denmark", FixtureDateTime = Convert.ToDateTime("2018-06-16 17:00:00")},
                    new Fixture { Group="D", Home="Croatia", Away="Nigeria", FixtureDateTime = Convert.ToDateTime("2018-06-16 20:00:00")},

                    new Fixture { Group="E", Home="Costa Rica", Away="Serbia", FixtureDateTime = Convert.ToDateTime("2018-06-17 13:00:00")},
                    new Fixture { Group="F", Home="Germany", Away="Mexico", FixtureDateTime = Convert.ToDateTime("2018-06-17 16:00:00")},
                    new Fixture { Group="E", Home="Brazil", Away="Switzerland", FixtureDateTime = Convert.ToDateTime("2018-06-17 19:00:00")},

                    new Fixture { Group="F", Home="Sweden", Away="South Korea", FixtureDateTime = Convert.ToDateTime("2018-06-18 13:00:00")},
                    new Fixture { Group="G", Home="Belgium", Away="Panama", FixtureDateTime = Convert.ToDateTime("2018-06-18 16:00:00")},
                    new Fixture { Group="G", Home="Tunisia", Away="England", FixtureDateTime = Convert.ToDateTime("2018-06-18 19:00:00")},

                    new Fixture { Group="H", Home="Columbia", Away="Japan", FixtureDateTime = Convert.ToDateTime("2018-06-19 13:00:00")},
                    new Fixture { Group="H", Home="Poland", Away="Senegal", FixtureDateTime = Convert.ToDateTime("2018-06-19 16:00:00")},
                    new Fixture { Group="A", Home="Russia", Away="Egypt", FixtureDateTime = Convert.ToDateTime("2018-06-19 19:00:00")},

                    new Fixture { Group="A",  Home="Portugal", Away="Morocco", FixtureDateTime = Convert.ToDateTime("2018-06-20 13:00:00")},
                    new Fixture { Group="B", Home="Uruguay", Away="Saudi Arabia", FixtureDateTime = Convert.ToDateTime("2018-06-20 16:00:00")},
                    new Fixture { Group="B", Home="Iran", Away="Spain", FixtureDateTime = Convert.ToDateTime("2018-06-20 19:00:00")},

                    new Fixture { Group="C", Home="Denmark", Away="Australia", FixtureDateTime = Convert.ToDateTime("2018-06-21 13:00:00")},
                    new Fixture { Group="C", Home="France", Away="Peru", FixtureDateTime = Convert.ToDateTime("2018-06-21 16:00:00")},
                    new Fixture { Group="D", Home="Argentina", Away="Croatia", FixtureDateTime = Convert.ToDateTime("2018-06-21 19:00:00")},

                    new Fixture { Group="E", Home="Brazil", Away="Costa Rica", FixtureDateTime = Convert.ToDateTime("2018-06-22 13:00:00")},
                    new Fixture { Group="D", Home="Nigeria", Away="Iceland", FixtureDateTime = Convert.ToDateTime("2018-06-22 16:00:00")},
                    new Fixture { Group="E", Home="Argentina", Away="Croatia", FixtureDateTime = Convert.ToDateTime("2018-06-22 19:00:00")},

                    new Fixture { Group="G", Home="Belgium", Away="Tunisia", FixtureDateTime = Convert.ToDateTime("2018-06-23 13:00:00")},
                    new Fixture { Group="F", Home="South Korea", Away="Mexico", FixtureDateTime = Convert.ToDateTime("2018-06-23 16:00:00")},
                    new Fixture { Group="F", Home="Germany", Away="Sweden", FixtureDateTime = Convert.ToDateTime("2018-06-23 19:00:00")},

                    new Fixture { Group="G", Home="England", Away="Panama", FixtureDateTime = Convert.ToDateTime("2018-06-24 13:00:00")},
                    new Fixture { Group="H", Home="Japan", Away="Senegal", FixtureDateTime = Convert.ToDateTime("2018-06-24 16:00:00")},
                    new Fixture { Group="H", Home="Poland", Away="Columbia", FixtureDateTime = Convert.ToDateTime("2018-06-24 19:00:00")},

                    // match day 3:

                    new Fixture { Group="A", Home="Saudi Arabia", Away="Egypt", FixtureDateTime = Convert.ToDateTime("2018-06-25 15:00:00")},
                    new Fixture { Group="A", Home="Uruguay", Away="Russia", FixtureDateTime = Convert.ToDateTime("2018-06-25 15:00:00")},

                    new Fixture { Group="B", Home="Iran", Away="Portugal", FixtureDateTime = Convert.ToDateTime("2018-06-25 19:00:00")},
                    new Fixture { Group="B", Home="Spain", Away="Morocco", FixtureDateTime = Convert.ToDateTime("2018-06-25 19:00:00")},

                    new Fixture { Group="C", Home="Australia", Away="Peru", FixtureDateTime = Convert.ToDateTime("2018-06-26 15:00:00")},
                    new Fixture { Group="C", Home="Denmark", Away="France", FixtureDateTime = Convert.ToDateTime("2018-06-26 15:00:00")},

                    new Fixture { Group="D", Home="Nigeria", Away="Argentina", FixtureDateTime = Convert.ToDateTime("2018-06-26 19:00:00")},
                    new Fixture { Group="D", Home="Iceland", Away="Croatia", FixtureDateTime = Convert.ToDateTime("2018-06-26 19:00:00")},

                    new Fixture { Group="E", Home="South Korea", Away="Germany", FixtureDateTime = Convert.ToDateTime("2018-06-27 15:00:00")},
                    new Fixture { Group="E", Home="Mexico", Away="Sweden", FixtureDateTime = Convert.ToDateTime("2018-06-27 15:00:00")},

                    new Fixture { Group="F", Home="Switzerland", Away="Costa Rica", FixtureDateTime = Convert.ToDateTime("2018-06-27 19:00:00")},
                    new Fixture { Group="F", Home="Serbia", Away="Brazil", FixtureDateTime = Convert.ToDateTime("2018-06-26 19:00:00")},

                    new Fixture { Group="G", Home="Senegal", Away="Columbia", FixtureDateTime = Convert.ToDateTime("2018-06-28 15:00:00")},
                    new Fixture { Group="G", Home="Japan", Away="Poland", FixtureDateTime = Convert.ToDateTime("2018-06-28 15:00:00")},

                    new Fixture { Group="H", Home="England", Away="Belgium", FixtureDateTime = Convert.ToDateTime("2018-06-28 19:00:00")},
                    new Fixture { Group="H", Home="Panama", Away="Tunisia", FixtureDateTime = Convert.ToDateTime("2018-06-28 19:00:00")},

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