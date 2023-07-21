using Infrastructure;
using Application;
using API.Endpoints;
using API.Configurations;
using API.BackgroundServices;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCookiePolicy(opt =>
{
  opt.MinimumSameSitePolicy = SameSiteMode.None;
  opt.Secure = CookieSecurePolicy.Always;
  opt.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
});

builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Services.ConfigureApplication();

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.ConfigureApiService();

builder.Services.ConfigureCronJob();

builder.Services.AddHostedService<RedisIndexCreationService>();
builder.Services.AddHostedService<DatabaseMigrationService>();

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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.SetupIdentityEndpoint();
app.SetupProductEndPoint();
app.SetupSaleEndpoint();
app.SetupBasketEndpoint();

app.Run();