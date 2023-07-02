using System.Diagnostics;

namespace ForeheadApi;

public static class DiagnosticsConfig
{
    public const string ServiceName = "ForeheadApi";
    public static readonly ActivitySource ActivitySource = new ActivitySource(ServiceName);
}
