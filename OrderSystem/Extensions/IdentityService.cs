using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Service;
using OrderSystem.Repository.Data;
using OrderSystem.Service;
using System.Text;

namespace OrderSystem.PL.Extensions
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OrderManagementDbContext>();

            Services.AddAuthentication(Option =>
            {
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // Default schema for all end-points is bearer
                Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;     /* No end-points will called without bearer schema
                                                                                              Any Request Must Have Bearer Schema */

            })
                .AddJwtBearer(option =>     // validation bearer schema to handle it (it's not handled)
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),

                    };
                });


            return Services;
        }
    }
}
