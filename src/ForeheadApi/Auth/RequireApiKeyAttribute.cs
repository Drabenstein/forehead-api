using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ForeheadApi.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RequireApiKeyAttribute : ServiceFilterAttribute
{
    public RequireApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
    {
    }

    internal class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ApiKeySettings apiKeySettings;

        public ApiKeyAuthorizationFilter(IOptionsMonitor<ApiKeySettings> apiKeySettingsOptions)
        {
            apiKeySettings = apiKeySettingsOptions.CurrentValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actualApiKey = GetActualApiKey(context.HttpContext);

            var expectedApiKey = GetExpectedApiKey();

            if (!IsApiKeyValid(expectedApiKey, actualApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private string GetActualApiKey(HttpContext context)
        {
            return context.Request.Headers[HeaderNames.ApiKeyHeaderName];
        }

        private string GetExpectedApiKey()
        {
            return apiKeySettings.ApiKey;
        }

        private static bool IsApiKeyValid(string apiKey, string submittedApiKey)
        {
            if (string.IsNullOrEmpty(submittedApiKey)) return false;

            var apiKeySpan = MemoryMarshal.Cast<char, byte>(apiKey.AsSpan());

            var submittedApiKeySpan = MemoryMarshal.Cast<char, byte>(submittedApiKey.AsSpan());

            return CryptographicOperations.FixedTimeEquals(apiKeySpan, submittedApiKeySpan);
        }
    }
}
