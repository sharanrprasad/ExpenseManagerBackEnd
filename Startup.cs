using System;
using ExpenseManagerBackEnd.Models.DbModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExpenseManagerBackEnd.Contracts;
using ExpenseManagerBackEnd.Extensions;
using ExpenseManagerBackEnd.Repositories;


namespace ExpenseManagerBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.RegisterJwtAuthentication();
            services.AddDbContext<ExpenseManagerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("Authorization")) {
                    Console.WriteLine("[TestMiddleware]  Auth Token - " + context.Request.Headers["Authorization"]);
                }
               
                Console.WriteLine(context.Request.Body);
                await next.Invoke();
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}