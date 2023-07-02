using ForeheadApi.Infrastructure;
using ForeheadApi.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services
    .AddDbContextPool<ForeheadDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("ForeheadDb")));
builder.Services.AddOpenTelemetryServices();

const string FrontendCorsPolicyName = "Frontend";

builder.Services
    .AddCors(
        opt => opt.AddPolicy(
            FrontendCorsPolicyName,
            policy => policy.AllowAnyMethod()
                .WithOrigins(builder.Configuration.GetSection("CorsOrigin").Get<string[]>() ?? Array.Empty<string>())));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(FrontendCorsPolicyName);

app.UseHealthChecks("/api/health");

app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();

app.Run();
