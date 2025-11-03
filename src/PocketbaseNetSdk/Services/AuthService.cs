using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Models.Base;
using PocketbaseNetSdk.Services.Base;

namespace PocketbaseNetSdk.Services
{
    public class AuthService<AuthModel> : BaseCrudService<AuthModel>, IAuthService
        where AuthModel : BaseAuthModel, new()
    {
        readonly ILogger<AuthService<AuthModel>>? _authLogger;
        private readonly ITokenService _tokenService;

        public override string CollectionName => "users";

        public override string BasePath(string? path = null)
        {
            if (path != null)
            {
                return $"/api/collections/{CollectionName}/{path}";
            }

            return $"/api/collections/{CollectionName}";
        }

        public AuthService(PocketbaseClient client, ITokenService tokenservice, ILogger<AuthService<AuthModel>>? logger = null) : base(client, logger)
        {
            _authLogger = logger;
            _tokenService = tokenservice;
        }

        public async Task<Result<TokenResponse>> AuthWithPassword(PasswordLoginRequest request)
        {
            _authLogger?.LogTrace("AuthWithPassword called");

            var response = await Client.SendJsonRequestAsync<TokenResponse, PasswordLoginRequest>(
                HttpMethod.Post,
                BasePath("auth-with-password"),
                request)
                .ConfigureAwait(false);

            if(response == null)
            {
                _authLogger?.LogError("AuthWithPassword response is null");
                throw new InvalidOperationException("AuthWithPassword response is null");
            }

            if (response.IsSuccess && response.Value is not null)
            {
                _tokenService.Token = response.Value.Token;
                _authLogger?.LogTrace("Token set successfully");

                return Result<TokenResponse>.Success(response.Value);
            }

            return Result<TokenResponse>.Failure(response.Error ?? "Authentication failed");
        }

        public async Task<Result<TokenResponse>> AuthRefresh()
        {
            _authLogger?.LogTrace("{Name} - AuthRefresh called", nameof(AuthService<AuthModel>));

            var response = await Client.SendJsonRequestAsync<TokenResponse, object>(
                HttpMethod.Post,
                BasePath("auth-refresh"))
                .ConfigureAwait(false);

            if(response == null)
            {
                _authLogger?.LogError("AuthRefresh response is null");
                throw new InvalidOperationException("AuthRefresh response is null");
            }

            if (response.IsSuccess && response.Value is not null)
            {
                _tokenService.Token = response.Value.Token;
                _authLogger?.LogTrace("Token set successfully");

                return Result<TokenResponse>.Success(response.Value);
            }

            return Result<TokenResponse>.Failure(response.Error ?? "Authentication failed");
        }

        public async Task<string?> GetToken(bool refresh = false, bool validateToken = false)
        {
            _authLogger?.LogTrace("GetToken called");

            if (refresh)
            {
                await AuthRefresh().ConfigureAwait(false);
            }

            if(validateToken)
            {
                bool IsTokenValid = _tokenService.IsValid();

                if (!IsTokenValid)
                {
                    return null;
                }
            }

            return _tokenService.Token!;
        }

        public Result SetToken(string token)
        {
            try
            {
                _tokenService.Token = token;
            }
            catch (Exception)
            {
                return Result.Failure("Failed to set token");
            }

            return Result.Success();
        }
    }
}
