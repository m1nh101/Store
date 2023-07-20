using Application.Users.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Configurations;

public static class IdentityConfiguration
{
  public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAuthentication(opt =>
    {
      opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
      .AddJwtBearer(opt =>
      {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]
          ?? throw new ArgumentNullException("no secret key found"));

        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateLifetime = true,
          ValidateIssuer = false,
          ValidateAudience = false,
        };

        opt.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            var token = context.Request.Cookies["access_token"]
              ?? context.Request.Headers["Authorization"].ToString()
              ?? string.Empty;

            context.Token = token.Replace("Bearer ", string.Empty);

            return Task.CompletedTask;
          }
        };

      });

    services.AddAuthorization(opt =>
    {
      opt.AddPolicy("SuperUser", policy =>
      {
        policy.RequireRole("admin")
          .RequireAuthenticatedUser();
      });

      opt.AddPolicy("SignedInUser", policy =>
      {
        policy.RequireAuthenticatedUser();
      });
    });

    services.AddOptions<JwtOption>().Bind(configuration.GetSection("Jwt"));

    return services;
  }
}
