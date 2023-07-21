using Microsoft.Extensions.Options;

namespace ForeheadApi.Auth;

public class ApiKeySettings
{
    public string ApiKey { get; set; } = "";
}

public class ApiKeySettingsValidator : IValidateOptions<ApiKeySettings>
{
    public ValidateOptionsResult Validate(string? name, ApiKeySettings options)
    {
        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            return ValidateOptionsResult.Fail("Api key cannot be empty");
        }

        return ValidateOptionsResult.Success;
    }
}
