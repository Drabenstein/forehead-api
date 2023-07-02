using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ForeheadApi.Infrastructure.Telemetry;

public static class TelemetryServiceCollectionExtensions
{
    public static void AddOpenTelemetryServices(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithTracing(
                tracerProviderBuilder => tracerProviderBuilder.AddSource(DiagnosticsConfig.ActivitySource.Name)
                    .ConfigureResource(res => res.AddService(DiagnosticsConfig.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddProcessor<TraceCustomProcessor>()
                    .AddNpgsql()
                    .AddOtlpExporter())
            .WithMetrics(
                metricsProviderBuilder => metricsProviderBuilder.ConfigureResource(
                    res => res.AddService(DiagnosticsConfig.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter());
    }
}
