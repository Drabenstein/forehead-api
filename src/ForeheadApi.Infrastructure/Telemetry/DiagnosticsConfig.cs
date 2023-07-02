using System.Diagnostics;

namespace ForeheadApi.Infrastructure.Telemetry;

public static class DiagnosticsConfig
{
    public const string ServiceName = "ForeheadApi";
    public static readonly ActivitySource ActivitySource = new ActivitySource(ServiceName);
}
