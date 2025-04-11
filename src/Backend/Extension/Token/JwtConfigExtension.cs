using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PaymentControl.Extension.Token
{
    public static class JwtConfigExtension
    {
        public static IServiceCollection AddAutentication(this IServiceCollection service, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JwtSetting:Secret"]);

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtSetting:Issuer"],
                    ValidAudience = configuration["JwtSetting:Audience"],
                    ClockSkew = TimeSpan.FromMinutes(1)
                    
                };
            });
            service.AddAuthorization();
            return service;
        }
    }
}
