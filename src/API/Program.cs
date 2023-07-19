using Infrastructure;
using Application;
using API.Endpoints;
using API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCookiePolicy(opt =>
{
  opt.MinimumSameSitePolicy = SameSiteMode.None;
  opt.Secure = CookieSecurePolicy.Always;
  opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
});

builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Services.ConfigureApplication();

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.ConfigureCronJob();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.SetupIdentityEndpoint();
app.SetupProductEndPoint();
app.SetupSaleEndpoint();

app.Run();