using ForeheadApi.Auth;
using ForeheadApi.Infrastructure;
using ForeheadApi.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using static ForeheadApi.Auth.RequireApiKeyAttribute;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptions<ApiKeySettings>()
    .BindConfiguration(nameof(ApiKeySettings))
    .ValidateOnStart();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
//builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services
    .AddDbContextPool<ForeheadDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("ForeheadDb")));
builder.Services.AddOpenTelemetryServices();
builder.Services.AddResponseCompression();

const string FrontendCorsPolicyName = "Frontend";

builder.Services
    .AddCors(
        opt => opt.AddPolicy(
            FrontendCorsPolicyName,
            policy => policy.AllowAnyMethod()
                .WithHeaders(HeaderNames.ApiKeyHeaderName)
                .WithOrigins(builder.Configuration.GetSection("CorsOrigin").Get<string[]>() ?? Array.Empty<string>())));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(FrontendCorsPolicyName);

app.UseHealthChecks("/api/health");

app.UseResponseCompression();
app.UseResponseCaching();
//app.UseAuthorization();
app.MapControllers();

app.Run();
