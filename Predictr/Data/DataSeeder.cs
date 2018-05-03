using Predictr.Models;
using System.Collections.Generic;

public class DataSeeder
{
    public static void SeedCountries(Microsoft.EntityFrameworkCore.DbContext context)
    {
            var teams = new List<Team>
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
                new Team { Name = "Senegal" },
            };
            context.AddRange(teams);
            context.SaveChanges();
        }
}