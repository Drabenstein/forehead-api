using OpenTelemetry;
using System.Diagnostics;

namespace ForeheadApi.Infrastructure.Telemetry;

internal class TraceCustomProcessor : BaseProcessor<Activity>
{
    public override void OnEnd(Activity activity)
    {
        if (IsHealthOrMetricsEndpoint(activity.DisplayName))
        {
            activity.ActivityTraceFlags &= ~ActivityTraceFlags.Recorded;
        }
    }

    private static bool IsHealthOrMetricsEndpoint(string displayName)
    {
        if (string.IsNullOrEmpty(displayName))
        {
            return false;
        }
        return displayName.StartsWith("/api/health");
    }

}
