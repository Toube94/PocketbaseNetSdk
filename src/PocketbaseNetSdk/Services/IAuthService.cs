using PocketbaseNetSdk.Models;

namespace PocketbaseNetSdk.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Login with email/username and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<Result<TokenResponse>> AuthWithPassword(PasswordLoginRequest request);
        /// <summary>
        /// Refresh the authentication token
        /// </summary>
        /// <returns></returns>
        public Task<Result<TokenResponse>> AuthRefresh();
        /// <summary>
        /// Get the current authentication token
        /// </summary>
        /// <param name="refresh"></param>
        /// <param name="validateToken"></param>
        /// <returns></returns>
        public Task<string?> GetToken(bool refresh = false, bool validateToken = false);

        public Result SetToken(string token);
    }
}
