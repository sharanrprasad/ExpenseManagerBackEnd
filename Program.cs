using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManagerBackEnd.DevScripts;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExpenseManagerBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ExpenseManagerContext>();
                    context.Database.EnsureCreated();
                    DBInitialiser.Initialise(context);

                }
                catch (Exception e)
                {
                    Console.WriteLine("An Error Occured while seeding the database"+ e.ToString());
                   
                }
            }
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}