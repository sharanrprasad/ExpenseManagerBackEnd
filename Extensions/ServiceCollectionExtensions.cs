using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagerBackEnd.Extensions
{
    public static  class ServiceCollectionExtensions
    {
        public static void RegisterJwtAuthentication(this IServiceCollection services)
        {
           
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime =  true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ExpenseManager!SWEN325#er"))
             
                };
            });
           
        }
    }
}