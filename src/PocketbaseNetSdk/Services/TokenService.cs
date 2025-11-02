using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Models.Base;
using System.Text;
using System.Text.Json;

namespace PocketbaseNetSdk.Services
{
    public class TokenService(ILogger<TokenService>? _Logger = null) : ITokenService
    {
        private string? _token;

        public string? Token
        {
            get
            {
                return _token;
            }

            set
            {
                _Logger?.LogTrace("Setting token....");

                var isValid = IsValid(value);

                if (!isValid)
                {
                    _Logger?.LogWarning("Attempted to set an invalid token.");
                    throw new ArgumentException("The provided token is not valid.", nameof(value));
                }

                _token = value;
                _Logger?.LogDebug("Token set successfully.");
            }
        }

        public bool IsValid()
        {
            return IsValid(Token!);
        }

        private static byte[] ParsePayload(string payload)
        {
            switch (payload.Length % 4)
            {
                case 2:
                    payload += "==";
                    break;
                case 3:
                    payload += "=";
                    break;
            }

            return Convert.FromBase64String(payload);
        }
        private static bool IsValid(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var parts = token.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                return false;
            }

            string rawPayload = parts[1];
            string payload = Encoding.UTF8.GetString(ParsePayload(rawPayload));
            var encoded = JsonSerializer.Deserialize<IDictionary<string, object>>(payload)!;

            if (encoded["exp"] is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
            {
                var exp = jsonElement.GetInt32();
                var expiredAt = DateTimeOffset.FromUnixTimeSeconds(exp);
                return expiredAt > DateTimeOffset.Now;
            }

            return false;
        }

        public void ClearToken()
        {
            Token = null;
        }
    }
}
