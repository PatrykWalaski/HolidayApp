using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {

            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Countries.Any())
                {
                    var data =
                        File.ReadAllText(path + @"/Data/SeedData/countries.json");

                    var list = JsonSerializer.Deserialize<List<Country>>(data);

                    foreach (var item in list)
                    {
                        context.Countries.Add(item);
                    }

                    await context.SaveChangesAsync();
                };

                if (!context.MealPlans.Any())
                {
                    var data =
                        File.ReadAllText(path + @"/Data/SeedData/meals.json");

                    var list = JsonSerializer.Deserialize<List<MealPlan>>(data);

                    foreach (var item in list)
                    {
                        context.MealPlans.Add(item);
                    }

                    await context.SaveChangesAsync();
                };

                if (!context.TravelAgencies.Any())
                {
                    var data =
                        File.ReadAllText(path + @"/Data/SeedData/agencies.json");

                    var list = JsonSerializer.Deserialize<List<TravelAgency>>(data);

                    foreach (var item in list)
                    {
                        context.TravelAgencies.Add(item);
                    }

                    await context.SaveChangesAsync();
                };

                if (!context.Holidays.Any())
                {
                    var data =
                        File.ReadAllText(path + @"/Data/SeedData/holidays.json");

                    var list = JsonSerializer.Deserialize<List<Holiday>>(data);

                    foreach (var item in list)
                    {
                        context.Holidays.Add(item);
                    }

                    await context.SaveChangesAsync();
                };
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}