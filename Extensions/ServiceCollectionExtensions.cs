using System;
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
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ExpenseManager!SWEN325#er"))
             
                };
            });
           
        }
    }
}